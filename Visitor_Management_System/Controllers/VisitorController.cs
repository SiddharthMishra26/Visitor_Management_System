using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Visitor_Management_System.Interface;
using Visitor_Management_System.Models;

namespace Visitor_Management_System.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VisitorController : ControllerBase
    {
        private readonly IVisitorService _visitorService;
        public VisitorController(IVisitorService visitorService)
        {
            _visitorService = visitorService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateVisitor(VisitorModel visitorModel)
        {
            var response = await _visitorService.CreateVisitor(visitorModel);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> GetAllVisitor()
        {
            var response = await _visitorService.GetAllVisitor();
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> ReadVisitorByUId(string UId)
        {
            var response = await _visitorService.ReadVisitorByUId(UId);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateVisitor(VisitorModel visitorModel)
        {
            var response = await _visitorService.UpdateVisitorByUId(visitorModel);
            return Ok(response);
        }

        [HttpPost]
        public async Task<string> DeleteVisitorByUId(string UId)
        {
            var response = await _visitorService.DeleteVisitorByUId(UId);
            return response;
        }
    }
}
