using Microsoft.AspNetCore.Http;
using System.Text;
using System.Threading.Tasks;

namespace Business.Util.Extensions
{
    public static class HttpContextExtensions
    {
        public static async Task MakeErrorResponse(this HttpContext context, int statusCode, dynamic error)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            byte[] buffer = Encoding.UTF8.GetBytes((new { Error = error }).ToJson());
            await context.Response.Body.WriteAsync(buffer, 0, buffer.Length);
        }

    }
}
