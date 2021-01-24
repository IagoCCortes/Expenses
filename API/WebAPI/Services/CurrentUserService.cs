using Expenses.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Expenses.WebAPI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.Request?.Headers["user"];
        }

        public string UserId { get; set; }
    }
}
