using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialApp.Data;
using SocialApp.DTOs;
using SocialApp.Entities;
using SocialApp.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace SocialApp.Controllers
{
    public class AccountController : BaseController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public AccountController(DataContext context, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _context = context;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            if (await UserExists(registerDTO.UserName)) return BadRequest("Username Already Taken!");
            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                UserName = registerDTO.UserName.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
                PasswordSalt = hmac.Key
            };
            _context.Add(user);
            await _context.SaveChangesAsync();
            return new UserDTO
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var registeredUser = await _context.Users.SingleOrDefaultAsync(user => user.UserName == loginDTO.UserName);
            if (registeredUser == null) Unauthorized("User not exists");

            using var hmacKey = new HMACSHA512(registeredUser.PasswordSalt);

          var LoginComputeHash = hmacKey.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

            for (int i = 0; i < LoginComputeHash.Length; i++)
            {
                if (LoginComputeHash[i] != registeredUser.PasswordHash[i]) return Unauthorized("Password not matched");
            }

            return new UserDTO
            {
                UserName = registeredUser.UserName,
                Token = _tokenService.CreateToken(registeredUser)
            };
        }
        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(u => u.UserName == username.ToLower());
        }
    }
}