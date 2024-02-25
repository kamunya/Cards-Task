using System;

namespace Cards.Errors
{
    public class APIResponce
    {

        public int statusCode { get; set; }
        public string Message { get; set; }

        public APIResponce(int StatusCode, string  message = null)
        {
            statusCode = StatusCode;
            Message = message ?? DefaultStatusCodeMessage(StatusCode);
        }
        private string DefaultStatusCodeMessage(int StatusCode)
        {
            return StatusCode switch
            {
                400 => "Bad request",
                401 => "Not authorized",
                404 => "Resource not found",
                500 => "Internal server error.",
                0 => "Request failed",
                _ => throw new NotImplementedException()
            };
        }
    }
}
