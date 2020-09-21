
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiXYZ.Starter.Api.ActionFilters
{

    /// <summary>
    /// Action filter to check if the model state has no errors, prevents further processing if state is not correct.
    /// </summary>
    public class ValidateModelStateActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        { 
            //no action here
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);               
            }
        }       
    }
}
