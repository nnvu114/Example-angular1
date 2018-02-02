using System;
using System.Net;

namespace Petronas.SampleApp.Service.Configuration
{
    public class CustomException : Exception
    {
        public string ErrorCode { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public CustomException(string errorCode, string errorMessage) : base(errorMessage)
        {
            StatusCode = HttpStatusCode.BadRequest;
            ErrorCode = errorCode;
        }

        public CustomException(HttpStatusCode statusCode, string errorCode, string errorMessage) : base(errorMessage)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
        }
    }
}
