using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using SonarWave.Application.Models;
using SonarWave.Core.Entities;
using SonarWave.Core.Enums;
using SonarWave.Core.EventArgs;
using SonarWave.Core.Extensions;
using SonarWave.Core.Interfaces;
using SonarWave.Core.Models.File;
using SonarWave.Core.Models.User;
using SonarWave.Core.Objects;
using System.Net;
using File = SonarWave.Core.Entities.File;

namespace SonarWave.Application.Hubs
{
    public class ConnectionHub : Hub
    {
        private readonly IUserService _userService;
        private readonly IRoomService _roomService;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public ConnectionHub(IUserService userService, IRoomService roomService, IFileService fileService, IMapper mapper)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _roomService = roomService ?? throw new ArgumentNullException(nameof(roomService));
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            _roomService.OnUserJoinedRoomAsync += OnUserJoinedRoomAsync;
            _roomService.OnUserLeftRoomAsync += OnUserLeftRoomAsync;
        }

        #region OnConnectedAsync

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            if (httpContext == null)
                return;

            bool validIp = IPAddress.TryParse(httpContext.Request.Headers["remote-ip-address"].ToString(), out IPAddress? address);
            if (!validIp)
                address = httpContext.Connection.RemoteIpAddress;

            var request = new CreateUserRequest()
            {
                ConnectionId = Context.ConnectionId,
                RemoteIpAddress = address != null ? address.ToString() : string.Empty,
                PlatformType = httpContext.Request.Headers["platform-type"].ToString().ToEnum<PlatformType>()
            };

            var result = await _userService.AddUserAsync(request);

            if (result.Succeeded)
                await base.OnConnectedAsync();
        }

        #endregion OnConnectedAsync

        #region OnDisconnectedAsync

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await _roomService.LeaveRoomAsync(Context.ConnectionId);
            await _userService.RemoveUserAsync(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }

        #endregion OnDisconnectedAsync

        #region JoinRoomAsync

        public async Task<Response<RoomItem>> JoinRoomAsync(string roomId = "")
        {
            Result<Room> result;

            if (string.IsNullOrEmpty(roomId))
            {
                result = await _roomService.JoinRoomAsync(Context.ConnectionId);
            }
            else
            {
                result = await _roomService.JoinRoomAsync(Context.ConnectionId, roomId);
            }

            if (result.Succeeded)
            {
                return Response<RoomItem>.Ok(_mapper.Map<RoomItem>(result.Value));
            }

            return result.Fault.ErrorType switch
            {
                ErrorType.NotFound => Response<RoomItem>.NotFound(result.Fault.ErrorMessage),
                _ => Response<RoomItem>.BadRequest(result.Fault.ErrorMessage),
            };
        }

        #endregion JoinRoomAsync

        #region LeaveRoomAsync

        public async Task<Response<bool>> LeaveRoomAsync()
        {
            var result = await _roomService.LeaveRoomAsync(Context.ConnectionId);

            if (result.Succeeded)
            {
                return Response<bool>.NoContent(result.Value);
            }

            return Response<bool>.BadRequest(result.Fault.ErrorMessage);
        }

        #endregion LeaveRoomAsync

        #region OnUserJoinedRoomAsync

        private async Task OnUserJoinedRoomAsync(UserJoinedRoomEventArgs args)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, args.RoomId);
            await Clients.OthersInGroup(args.RoomId).SendAsync("OnUserJoinedRoom", _mapper.Map<UserItem>(args.User));
        }

        #endregion OnUserJoinedRoomAsync

        #region OnUserLeftRoomAsync

        private async Task OnUserLeftRoomAsync(UserLeftRoomEventArgs args)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, args.RoomId);
            await Clients.OthersInGroup(args.RoomId).SendAsync("OnUserLeftRoom", args.ConnectionId);
        }

        #endregion OnUserLeftRoomAsync

        #region FileTransferRequestAsync

        public async Task<Response<FileItem>> FileTransferRequestAsync(CreateFileRequest request)
        {
            var result = await _fileService.AddFileAsync(Context.ConnectionId, request);

            if (result.Succeeded)
            {
                await Clients.Client(result.Value.RecipientId).SendAsync("OnFileTransferRequest", _mapper.Map<FileItem>(result.Value));
                return Response<FileItem>.Created(_mapper.Map<FileItem>(result.Value));
            }

            return result.Fault.ErrorType switch
            {
                ErrorType.NotFound => Response<FileItem>.NotFound(result.Fault.ErrorMessage),
                _ => Response<FileItem>.BadRequest(result.Fault.ErrorMessage),
            };
        }

        #endregion FileTransferRequestAsync

        #region FileTransferRespondAsync

        public async Task<Response<FileItem>> FileTransferRespondAsync(UpdateFileRequest request)
        {
            var result = await _fileService.UpdateFileAsync(Context.ConnectionId, request);

            if (result.Succeeded)
            {
                await Clients.Client(result.Value.SenderId).SendAsync("OnFileTransferRespond", _mapper.Map<FileItem>(result.Value));
                return Response<FileItem>.Ok(_mapper.Map<FileItem>(result.Value));
            }

            return result.Fault.ErrorType switch
            {
                ErrorType.NotFound => Response<FileItem>.NotFound(result.Fault.ErrorMessage),
                _ => Response<FileItem>.BadRequest(result.Fault.ErrorMessage),
            };
        }

        #endregion FileTransferRespondAsync

        #region TransferFileAsync

        public async Task TransferFileAsync(string fileId, IAsyncEnumerable<byte[]> chunks)
        {
            var file = await _fileService.GetFileAsync(Context.ConnectionId, fileId);

            if (file == null)
                return;

            if (file.Acceptance != TransferAcceptance.Accepted)
                return;

            await foreach (var chunk in chunks)
            {
                await Clients.Client(file.RecipientId).SendAsync("ReceiveFile", new FileChunk()
                {
                    FileId = file.Id,
                    Chunk = chunk
                });
            }

            await _fileService.RemoveFileAsync(Context.ConnectionId, fileId);
        }

        #endregion TransferFileAsync
    }
}