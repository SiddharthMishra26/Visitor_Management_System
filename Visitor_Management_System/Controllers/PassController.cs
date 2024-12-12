using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Visitor_Management_System.Interface;
using Visitor_Management_System.Models;

namespace Visitor_Management_System.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PassController : ControllerBase
    {
        private readonly IPassService _passService;
        public PassController(IPassService passService)
        {
            _passService = passService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePass(PassModel passModel)
        {
            var response = await _passService.CreatePass(passModel);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> GetAllPass()
        {
            var response = await _passService.GetAllPass();
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> ReadPassByUId(string UId)
        {
            var response = await _passService.ReadPassByUId(UId);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePassByUId(PassModel passModel)
        {
            var response = await _passService.UpdatePassByUId(passModel);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> GetPassByStatus(string status)
        {
            var response = await _passService.GetPassByStatus(status);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GeneratePassPdf(string UId)
        {
            var pdfFilePath = await _passService.GeneratePassPdf(UId);
            var fileBytes = System.IO.File.ReadAllBytes(pdfFilePath);
            return File(fileBytes, "application/pdf", $"{UId}_Pass.pdf");
        }

        [HttpGet]
        public async Task<IActionResult> GeneratePassesByStatusPdf(string status)
        {
            var pdfFilePath = await _passService.GeneratePassesByStatusPdf(status);
            var fileBytes = System.IO.File.ReadAllBytes(pdfFilePath);
            return File(fileBytes, "application/pdf", $"{status}_Passes.pdf");
        }


        //[HttpPost]
        //public async Task<IActionResult> SendPassToVisitor(string UId, string email)
        //{
        //    if (string.IsNullOrEmpty(UId) || string.IsNullOrEmpty(email))
        //        return BadRequest("UId and email must be provided.");

        //    var result = await _passService.SendPassToVisitor(UId, email);
        //    if (result)
        //        return Ok("Email sent successfully.");
        //    else
        //        return StatusCode(500, "Failed to send the email.");
        //}

    }
}
