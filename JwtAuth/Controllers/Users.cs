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
    public class User : ControllerBase
    {
        IUser _user;

        public User(IUser user)
        {
            _user = user;
        }

        [HttpPost("CreateUser")]

        public async Task<DataResult> CreateUser(UserViewModel userViewModel)
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
                    object data = await _user.CreateUser(userViewModel);

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

        [HttpGet("Admins")]
        [Authorize(Roles = "Developer")]
        public DataResult Authorize()
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
                    object data =  _user.Authorize();

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
