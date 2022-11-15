#pragma warning disable CS1591

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPlayground.Controllers;
using WebApiPlayground.Model;

namespace WebApiPlayground.Services
{
    public interface IUserService
    {
        /**
         * <summary>从当前请求中获取用户的username。</summary>
         */
        string GetName(WithDbControllerBase controller);
        /**
         * <summary>Get the current user entity.</summary>
         */
        Task<User> GetUser(WithDbControllerBase controller);
    }
    public class UserService : IUserService
    {

        public string GetName(WithDbControllerBase controller)
        {
            var identity = controller.User.Identity;
            if (identity == null) throw new Exception("User hasn't authenticated.");
            return identity.Name ?? "";
        }

        public async Task<User> GetUser(WithDbControllerBase controller)
        {
            var username = GetName(controller);
            return await controller.DbContext.Users.SingleAsync(u => u.Username == username);
        }
    }
}
