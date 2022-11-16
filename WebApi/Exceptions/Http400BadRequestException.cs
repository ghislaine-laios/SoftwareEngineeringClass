#pragma warning disable CS1591
namespace WebApi.Exceptions
{
    public class Http400BadRequestException: HttpException
    {
        public Http400BadRequestException(string message) : base(message, StatusCodes.Status400BadRequest)
        {
        }
    }
}
