using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Exceptions
{
    public class BaseHttpException : Exception
    {
        public int StatusCode { get; set; }
        public object Body { get; set; }

        public BaseHttpException(int statusCode)
        {
            this.StatusCode = statusCode;
        }

        public BaseHttpException(int statusCode, dynamic body) : this(statusCode)
        {
            this.Body = body;
        }
    }
}
