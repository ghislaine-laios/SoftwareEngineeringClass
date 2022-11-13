#pragma warning disable CS1591

using Microsoft.AspNetCore.Mvc;

namespace WebApiPlayground.Services
{
    public interface IUserService
    {
        /**
         * <summary>从当前请求中获取用户的username。</summary>
         */
        string GetName(ControllerBase controller);
    }
    public class UserService: IUserService
    {
        
        public string GetName(ControllerBase controller)
        {
            var identity = controller.User.Identity;
            if (identity == null) throw new Exception("User hasn't authenticated.");
            return identity.Name ?? "";
        }
    }
}
