using JwtAuth.Helper;
using JwtAuth.Models;
using JwtAuth.ViewModels;

namespace JwtAuth.IServices
{
    public interface ILogin
    {
        public string Generate(User user);
        Task<User> Authenticate(UserLogin login);
        Task<string>LoginUser(UserLogin login);
    }
}
