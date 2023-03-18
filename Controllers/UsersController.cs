using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialApp.Data;
using SocialApp.Entities;
using SocialApp.Interfaces;

namespace SocialApp.Controllers
{
    /*[Authorize]*/
    public class UsersController : BaseController
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            return Ok(await _userRepository.GetUsersAsync());
        }

        [HttpGet("username")]
        public async Task<ActionResult<AppUser>> GetUserByUserName(string username)
        {
            return Ok( await _userRepository.GetUserByNameAsync(username));
        }
    }
}
