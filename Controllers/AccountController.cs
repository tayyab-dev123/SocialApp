using Microsoft.AspNetCore.Mvc;
using SocialApp.Data;
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
        public async Task<ActionResult<AppUser>> Register(string username, string password)
        {
            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                UserName = username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
                PasswordSalt = hmac.Key
            };
            _context.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

    }
}