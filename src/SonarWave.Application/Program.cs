using SonarWave.Application.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
    options.ClientTimeoutInterval = TimeSpan.FromMinutes(10);
    options.MaximumReceiveMessageSize = 1000000000;
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapHub<TransferHub>("/hubs/transferhub");

app.Run();