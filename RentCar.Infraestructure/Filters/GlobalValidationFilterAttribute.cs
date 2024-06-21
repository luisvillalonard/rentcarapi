using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diversos.Infraestructure.Filters
{
    public class GlobalValidationFilterAttribute : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ActionArguments.Any(x => x.Value == null))
                context.Result = new BadRequestObjectResult("El objeto es nulo");

            if (!context.ModelState.IsValid)
            {
                Dictionary<string, string[]> arrErrors = new();
                foreach (var err in context.ModelState.Where(x => x.Value?.Errors.Count > 0))
                {
                    if (err.Value != null)
                        arrErrors.Add(err.Key, err.Value.Errors.Select(x => x.ErrorMessage).ToArray());
                }
                var responseObj = new
                {
                    StatusCode = "400",
                    Message = "Bad Request",
                    Errors = arrErrors
                };

                context.Result = new BadRequestObjectResult(responseObj);
                return;
            }

            await next();
        }
    }
}
