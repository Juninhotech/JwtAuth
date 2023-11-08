using JwtAuth.Helper;
using JwtAuth.Models;
using JwtAuth.ViewModels;

namespace JwtAuth.IServices
{
    public interface IUser
    {
        Task<Responses> CreateUser(UserViewModel userViewModel);
        //Task<UserViewModel> GetCurrentUser();
        Responses Authorize();
    }
}
