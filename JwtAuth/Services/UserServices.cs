using JwtAuth.Data;
using JwtAuth.Helper;
using JwtAuth.IServices;
using JwtAuth.Models;
using JwtAuth.ViewModels;
using System.Security.Claims;

namespace JwtAuth.Services
{
    public class UserServices : IUser
    {
        private IConfiguration _jWTDb;
        private readonly JWTDbContext _jWTDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public UserServices(IConfiguration jWTDb, JWTDbContext jWTDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _jWTDb = jWTDb;
            _jWTDbContext = jWTDbContext;
            _httpContextAccessor = httpContextAccessor;

        }
        public async Task<Responses> CreateUser(UserViewModel userViewModel)
        {
            var user = new User
            {
                Username = userViewModel.Username,
                Password = userViewModel.Password,
                GivenName = userViewModel.GivenName,
                Surname = userViewModel.Surname,
                Email = userViewModel.Email,
                Role = userViewModel.Role
            };
            await _jWTDbContext.Users.AddAsync(user);
            await _jWTDbContext.SaveChangesAsync();

            return new Responses
            {
                ResponseCode = 200,
                ResponseMessage = "User Added Successfully",
                ResponseResult = "Success",
            };
        }

        private UserViewModel GetCurrentUser()
        {
            try
            {
                var identity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;

                if (identity != null)
                {
                    var userClaims = identity.Claims;

                    return new UserViewModel
                    {
                        Username = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
                        Email = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value,
                        GivenName = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value,
                        Surname = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Surname)?.Value,
                        Role = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value


                    };

                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Responses Authorize()
        {
            try
            {
                var authorizedCurrentUser = GetCurrentUser();

                return new Responses
                {
                    ResponseCode = 200,
                    ResponseMessage = $"Hello {authorizedCurrentUser.GivenName}, you are in as {authorizedCurrentUser.Role}",
                    ResponseResult = "Success"
                };
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
