using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Visitor_Management_System.Interface;
using Visitor_Management_System.Models;

namespace Visitor_Management_System.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerService _managerService;
        public ManagerController(IManagerService managerService)
        {
            _managerService = managerService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateManager(ManagerModel managerModel)
        {
            var response = await _managerService.CreateManager(managerModel);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> GetAllManager()
        {
            var response = await _managerService.GetAllManager();
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> ReadManagerByUId(string UId)
        {
            var response = await _managerService.ReadManagerByUId(UId);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateManagerByUId(ManagerModel managerModel)
        {
            var response = await _managerService.UpdateManagerByUId(managerModel);
            return Ok(response);
        }

        [HttpPost]
        public async Task<string> DeleteManagerByUId(string UId)
        {
            var response = await _managerService.DeleteManagerByUId(UId);
            return response;
        }
    }
}
