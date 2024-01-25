using System;
using System.Net;

namespace Domain.Exceptions
{
    public class AppException : ApplicationException
    {
        public override string Message { get; }
        public HttpStatusCode HttpStatusCode { get; }

        public AppException(string message, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        {
            Message = message;
            HttpStatusCode = httpStatusCode;
        }
    }
}