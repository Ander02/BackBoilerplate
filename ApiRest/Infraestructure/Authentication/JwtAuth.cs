using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace ApiRest.Infraestructure.Authentication
{
    public class JwtAuth : AuthorizeAttribute
    {
        public JwtAuth()
        {
            this.AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
        }
    }
}
