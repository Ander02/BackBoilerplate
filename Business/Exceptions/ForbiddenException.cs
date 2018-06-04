using System.Net;

namespace Business.Exceptions
{
    public class ForbiddenException : BaseHttpException
    {
        public ForbiddenException() : base((int)HttpStatusCode.Forbidden) { }

        public ForbiddenException(dynamic body) : base((int)HttpStatusCode.Forbidden)
        {
            this.Body = body;
        }
    }
}
