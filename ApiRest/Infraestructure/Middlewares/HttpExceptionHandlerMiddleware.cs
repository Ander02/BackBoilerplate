using Business.Exceptions;
using Business.Util.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace ApiRest.Infraestructure.Middlewares
{
    public class HttpExceptionHandlerMiddleware
    {
        readonly RequestDelegate next;
        public HttpExceptionHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (BaseHttpException e)
            {
                await context.MakeErrorResponse(e.StatusCode, (string)e.Body);
            }
            catch (Exception e)
            {
                await context.MakeErrorResponse(400, e.Message);
            }
        }
    }
}
