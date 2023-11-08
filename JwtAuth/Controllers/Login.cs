using JwtAuth.Data;
using JwtAuth.Helper;
using JwtAuth.IServices;
using JwtAuth.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Login : ControllerBase
    {
        ILogin _login;

        public Login(ILogin login)
        {
            _login = login;
        }

        [AllowAnonymous]
        [HttpPost("Logins")]

        public async Task<DataResult> Logins(UserLogin login)
        {
            DataResult dataResult;

            try
            {
                if (!ModelState.IsValid)
                {
                    dataResult = new DataResult
                    {
                        StatusCode = "400",
                        Message = "Bad request",
                        Data = false,
                    };
                }


                try
                {
                    object data = await _login.LoginUser(login);

                    dataResult = new DataResult
                    {
                        StatusCode = "200",
                        Message = "Successful",
                        Data = data,
                    };
                }
                catch (Exception ex)
                {

                    dataResult = new DataResult
                    {
                        StatusCode = "404",
                        Message = ex.Message,
                        Data = false,
                    };
                }

            }
            catch (Exception ex)
            {

                dataResult = new DataResult
                {
                    StatusCode = "406",
                    Message = "Unknown error occur",
                    ExceptionErrorMessage = ex.Message,
                    Data = null,
                };
            }
            return dataResult;
        }
    }
}
