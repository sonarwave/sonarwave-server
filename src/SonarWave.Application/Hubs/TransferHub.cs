using Microsoft.AspNetCore.SignalR;

namespace SonarWave.Application.Hubs
{
    /// <summary>
    /// A hub for transferring related operation.
    /// </summary>
    public class TransferHub : Hub
    {
        #region OnConnectedAsync

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        #endregion OnConnectedAsync

        #region OnDisconnectedAsync

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

        #endregion OnDisconnectedAsync

        #region TransferFileAsync

        /// <summary>
        /// A stream for transferring a file to a specified client.
        /// </summary>
        /// <param name="connectionId">Represents the connectionId of a client.</param>
        /// <param name="chunks">Represents byte chunks.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// </returns>
        public async Task TransferFileAsync(string connectionId, IAsyncEnumerable<byte[]> chunks)
        {
            await foreach (var chunk in chunks)
            {
                await Clients.Client(connectionId).SendAsync("ReceiveFile", chunk);
            }
        }

        #endregion TransferFileAsync
    }
}