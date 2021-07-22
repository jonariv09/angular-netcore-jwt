using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using angular_netcore_jwt.Entities;
using angular_netcore_jwt.Helpers;
using angular_netcore_jwt.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace angular_netcore_jwt.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }

    public class UserService : IUserService
    {
        private List<User> _users = new List<User>
        {
            new User { Id = 1, FirstName = "Test", LastName = "User", UserName = "test", Password = "test" },
            new User { Id = 2, FirstName =  "ADMIN 1", LastName = "ADMIN 1", UserName = "admin1", Password = "admin1" },
            new User { Id = 2, FirstName =  "ADMIN 2", LastName = "ADMIN 2", UserName = "admin2", Password = "admin2" },
            new User { Id = 2, FirstName =  "ADMIN 3", LastName = "ADMIN 3", UserName = "admin3", Password = "admin3" },
        };
        
        private readonly AppSettings _appSettings;
        private IUserService _userServiceImplementation;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _users.SingleOrDefault(x => x.UserName == model.Username && x.Password == model.Password);
            if (user == null)
                return null;

            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public User GetById(int id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }


        private string generateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
    
}