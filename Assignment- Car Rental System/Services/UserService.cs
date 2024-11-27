using Assignment__Car_Rental_System.Models;
using Assignment__Car_Rental_System.Repositories;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Assignment__Car_Rental_System.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly string key;
        private readonly string issuer;
        private readonly string audience;
        public UserService(IUserRepo userRepo, IConfiguration iconfiguration)
        {
            _userRepo = userRepo;
            key = iconfiguration["JWT:Key"];
            issuer = iconfiguration["JWT:Issuer"];
            audience = iconfiguration["JWT:Audience"];
        }
        public bool RegisterUser(User user)
        {
            if(_userRepo.GetUserByEmail(user.Email)!=null) 
                return false;
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _userRepo.AddUser(user);
            return true;
        }
        public string AuthenticateUser(string email, string password)
        {
            var user=_userRepo.GetUserByEmail(email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
                return null;
            return GenerateToken(user.Name, user.Role);
        }
        public string GenerateToken(string username, string role)
        {
            var securityKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var cred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,username),
                new Claim("role", role),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            var token=new JwtSecurityToken(
                issuer : issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: cred
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
