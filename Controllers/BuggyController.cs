using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialApp.Data;
using SocialApp.Entities;

namespace SocialApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        private readonly DataContext _Context;

        public BuggyController(DataContext context)
        {
            _Context = context;
        }
        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {
            return "Secret";
        }
        [HttpGet("not-found")]

        public ActionResult<AppUser> GetNotFound()
        {
            var things = _Context.Users.Find(-1);
            if(things==null) return NotFound();
            return Ok(things);

        }
        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            var things = _Context.Users.Find(-1);
            var thingsToReturn = things.ToString();
            return thingsToReturn;
        }
        [HttpGet("bad-request")]
        public ActionResult<AppUser> GetBadRequest()
        {
            return BadRequest("Bad request");
        }
    }
}
