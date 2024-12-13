using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Visitor_Management_System.Models;
using System.Linq;

namespace Visitor_Management_System.ServiceFilters
{
    public class EnsurePassStatusFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Find the action parameter of type PassModel
            var param = context.ActionArguments.SingleOrDefault(p => p.Value is PassModel);

            if (param.Value == null)
            {
                // Return a BadRequest if PassModel is null
                context.Result = new BadRequestObjectResult("Pass model is required.");
                return;
            }

            PassModel passModel = (PassModel)param.Value;

            // If Status is not specified, set it to "Rejected"
            if (string.IsNullOrEmpty(passModel.Status) || passModel.Status != "Approved")
            {
                passModel.Status = "Rejected";
            }
            if (string.IsNullOrEmpty(passModel.Email))
            {
                passModel.Email = "xyz.gmail.com";
            }
            if (string.IsNullOrEmpty(passModel.Name))
            {
                passModel.Name = "xyz";
            }

            // Proceed with the next filter or action
            await next();
        }
    }
}