using System.Net;

namespace Business.Exceptions
{
    public class BadRequestException : BaseHttpException
    {
        public BadRequestException() : base((int)HttpStatusCode.BadRequest) { }

        public BadRequestException(dynamic body) : base((int)HttpStatusCode.BadRequest)
        {
            this.Body = body;
        }
    }
}
