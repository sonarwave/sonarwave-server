using Microsoft.AspNetCore.SignalR;
using SonarWave.Core.Enums;
using SonarWave.Core.Extensions;
using SonarWave.Core.Interfaces;
using SonarWave.Core.Models.User;

namespace SonarWave.Application.Hubs
{
    public class ConnectionHub : Hub
    {
        private readonly IUserService _userService;

        public ConnectionHub(IUserService userService)
        {
            _userService = userService;
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            if (httpContext == null)
                return;

            var request = new CreateUserRequest()
            {
                ConnectionId = Context.ConnectionId,
                RemoteIpAddress = httpContext.Request.Headers["remote-ip-address"].ToString(),
                PlatformType = httpContext.Request.Headers["platform-type"].ToString().ToEnum<PlatformType>()
            };

            var result = await _userService.AddUserAsync(request);

            if (result.Succeeded)
                await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await OnDisconnectedAsync(exception);
        }
    }
}