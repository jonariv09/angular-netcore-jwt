using System.ComponentModel.DataAnnotations;

namespace angular_netcore_jwt.Models
{
    public class AuthenticateRequest
    {
        [Required] 
        public string Username { get; set; }

        [Required] public string Password { get; set; }
    }
}