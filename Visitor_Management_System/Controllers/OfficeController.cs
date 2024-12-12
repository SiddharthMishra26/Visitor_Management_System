using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Visitor_Management_System.Interface;
using Visitor_Management_System.Models;

namespace Visitor_Management_System.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OfficeController : ControllerBase
    {
        private readonly IOfficeService _officeService;
        public OfficeController(IOfficeService officeService)
        {
            _officeService = officeService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOffice(OfficeModel officeModel)
        {
            var response = await _officeService.CreateOffice(officeModel);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> GetAllOffice()
        {
            var response = await _officeService.GetAllOffice();
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> ReadOfficeByUId(string UId)
        {
            var response = await _officeService.ReadOfficeByUId(UId);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOfficeByUId(OfficeModel officeModel)
        {
            var response = await _officeService.UpdateOfficeByUId(officeModel);
            return Ok(response);
        }

        [HttpPost]
        public async Task<string> DeleteOfficeByUId(string UId)
        {
            var response = await _officeService.DeleteOfficeByUId(UId);
            return response;
        }
    }
}
