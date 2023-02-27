using System.ComponentModel.DataAnnotations;

namespace SocialApp.DTOs
{
    public class UserDTO
    {
        public string UserName { get; set; }

        public string Token { get; set; }
    }
}