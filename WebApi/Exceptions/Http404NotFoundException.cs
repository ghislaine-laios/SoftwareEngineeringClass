#pragma warning disable CS1591
namespace WebApi.Exceptions
{
    public class Http404NotFoundException: HttpException
    {
        public Http404NotFoundException(string message) : base(message, StatusCodes.Status404NotFound)
        {
        }
    }
}
