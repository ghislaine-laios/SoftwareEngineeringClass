#pragma warning disable CS1591
namespace WebApi.Exceptions
{
    public class HttpException: Exception
    {
        public int StatusCode { get; }

        public HttpException(string message, int statusCode) : base(message)
        {
            this.StatusCode = statusCode;
        }
    }
}
