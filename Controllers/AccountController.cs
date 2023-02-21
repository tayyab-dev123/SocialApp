using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialApp.Data;
using SocialApp.DTOs;
using SocialApp.Entities;
using System.Security.Cryptography;
using System.Text;

namespace SocialApp.Controllers
{
    public class AccountController : BaseController
    {

        private readonly DataContext _context;

        public AccountController(DataContext context)
        {
            _context = context;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto)
        {
            if(await UserExists(registerDto.UserName)) return BadRequest("Username Already Taken!");
            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                UserName = registerDto.UserName.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };
            _context.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        private async Task<bool> UserExists(string username){
            return await _context.Users.AnyAsync(u => u.UserName == username.ToLower());
        }

    }
}