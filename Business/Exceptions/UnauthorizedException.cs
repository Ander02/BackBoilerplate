using System.Net;

namespace Business.Exceptions
{
    public class UnauthorizedException : BaseHttpException
    {
        public UnauthorizedException() : base((int)HttpStatusCode.Unauthorized) { }

        public UnauthorizedException(dynamic body) : base((int)HttpStatusCode.Unauthorized)
        {
            this.Body = body;
        }
    }
}
