using JwtAuth.Data;
using JwtAuth.Helper;
using JwtAuth.IServices;
using JwtAuth.Models;
using JwtAuth.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JwtAuth.Services
{
    public class LoginServices : ILogin
    {
        private IConfiguration _jWTDb;
        private readonly JWTDbContext _jWTDbContext;

        public LoginServices(IConfiguration jWTDb, JWTDbContext jWTDbContext)
        {
            _jWTDb = jWTDb;
            _jWTDbContext = jWTDbContext;

        }
        public async Task<User> Authenticate(UserLogin login)
        {
            try
            {
                var currentUser = await _jWTDbContext.Users.FirstOrDefaultAsync(x => x.Username == login.Username && x.Password == login.Password);

                if (currentUser != null)
                {
                    return currentUser;
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string Generate(User userLogin)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jWTDb["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userLogin.Username),
                    new Claim(ClaimTypes.Email, userLogin.Email),
                    new Claim(ClaimTypes.GivenName, userLogin.GivenName),
                    new Claim(ClaimTypes.Role, userLogin.Role),
                    new Claim(ClaimTypes.Surname, userLogin.Surname),
                };

                //Define the token object
                var token = new JwtSecurityToken(_jWTDb["Jwt:Issuer"],
               _jWTDb["Jwt:Audience"],
               claims,
               expires: DateTime.Now.AddMinutes(5),
               signingCredentials: credentials
               );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<string> LoginUser(UserLogin login)
        {
            try
            {
                var user = await Authenticate(login);

                //Check if user exist
                if(user != null)
                {
                    var token = Generate(user);
                    return token;
                }
                return "User not found";
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
