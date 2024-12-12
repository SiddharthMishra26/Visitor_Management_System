using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Visitor_Management_System.Interface;
using Visitor_Management_System.Models;

namespace Visitor_Management_System.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly ISecurityService _securityService;
        public SecurityController(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSecurity(SecurityModel securityeModel)
        {
            var response = await _securityService.CreateSecurity(securityeModel);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> GetAllSecurity()
        {
            var response = await _securityService.GetAllSecurity();
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> ReadSecurityByUId(string UId)
        {
            var response = await _securityService.ReadSecurityByUId(UId);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSecurityByUId(SecurityModel securityeModel)
        {
            var response = await _securityService.UpdateSecurityByUId(securityeModel);
            return Ok(response);
        }

        [HttpPost]
        public async Task<string> DeleteSecurityByUId(string UId)
        {
            var response = await _securityService.DeleteSecurityByUId(UId);
            return response;
        }
    }
}
