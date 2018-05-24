using Business.Util.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Linq;

namespace ApiRest.Infraestructure.Filters
{
    public class ValidationActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context) { }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var result = new ContentResult();
                string error = context.ModelState.Select(modelState => new
                {
                    Error = modelState.Value.Errors.Select(e => e.Exception != null ? e.Exception.Message : e.ErrorMessage)
                }).ToJson(new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

                result.Content = error;
                result.ContentType = "application/json";

                context.HttpContext.Response.StatusCode = 400;
                context.Result = result;
            }
        }
    }
}
