using Expenses.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Expenses.WebAPI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.Request?.Headers["usuario"];
        }

        public string UserId { get; set; }
    }
}
