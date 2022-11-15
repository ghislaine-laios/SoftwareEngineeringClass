#pragma warning disable CS1591
namespace WebApiPlayground.Exceptions
{
    public class Http403ForbiddenException: HttpException
    {
        public Http403ForbiddenException(string message) : base(message, StatusCodes.Status403Forbidden)
        {
        }
    }
}
