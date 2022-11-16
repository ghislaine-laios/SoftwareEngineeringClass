#pragma warning disable CS1591
namespace WebApi.Exceptions
{
    public class Http409ConflictException: HttpException
    {
        public Http409ConflictException(string message) : base(message, StatusCodes.Status409Conflict)
        {
        }
    }
}
