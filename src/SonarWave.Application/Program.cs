using SonarWave.Application.Hubs;
using SonarWave.Core.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterServicesFromAssemblies(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapHub<ConnectionHub>("/connectionhub");

app.Run();