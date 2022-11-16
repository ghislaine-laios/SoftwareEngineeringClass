#pragma warning disable CS1591
namespace WebApi.Exceptions
{
    public class Http403ForbiddenException: HttpException
    {
        public Http403ForbiddenException(string message) : base(message, StatusCodes.Status403Forbidden)
        {
        }
    }
}
