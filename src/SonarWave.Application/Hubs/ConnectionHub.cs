using Microsoft.AspNetCore.SignalR;
using SonarWave.Application.Enums;
using SonarWave.Application.Extensions;
using SonarWave.Application.Models.User;
using SonarWave.Application.Services;

namespace SonarWave.Application.Hubs
{
    public class ConnectionHub : Hub
    {
        private readonly UserService _userService;

        public ConnectionHub(UserService userService)
        {
            _userService = userService;
        }

        #region OnConnectedAsync

        public override async Task OnConnectedAsync()
        {
            HttpContext? httpContext = Context.GetHttpContext();

            if (httpContext == null)
                await Task.CompletedTask;

            CreateUserRequest request = new CreateUserRequest()
            {
                ConnectionId = Context.ConnectionId,
                RemoteIpAddress = httpContext.Request.Headers["remote-ip-address"].ToString(),
                PlatformType = httpContext.Request.Headers["platform-type"].ToString().ToEnum<PlatformType>()
            };

            bool result = await _userService.AddAsync(request);
            if (result)
                await base.OnConnectedAsync();
        }

        #endregion OnConnectedAsync

        #region OnDisconnectedAsync

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        #endregion OnDisconnectedAsync
    }
}