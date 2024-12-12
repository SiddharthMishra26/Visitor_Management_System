using Microsoft.AspNetCore.Http;
using Visitor_Management_System.Interface;
using Visitor_Management_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace Visitor_Management_System.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        public LoginController (ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<string> Login(LoginModel loginModel)
        {
            var response = await _loginService.Login(loginModel);
            return response;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAllLogin()
        //{
        //    var response = await _loginService.GetAllLogin();
        //    return Ok(response);
        //}
    }
}
