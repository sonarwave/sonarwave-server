using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using SonarWave.Application;
using SonarWave.Application.Hubs;
using SonarWave.Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(MapperInitializer));

builder.Services.AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();

builder.Services.AddTransient<UserService>();

builder.Services.AddDbContext<DatabaseContext>(opt =>
{
    opt.UseInMemoryDatabase(nameof(DatabaseContext));
});

builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
    options.ClientTimeoutInterval = TimeSpan.FromMinutes(10);
    options.MaximumReceiveMessageSize = 1000000000;
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapHub<ConnectionHub>("/connectionhub");

app.Run();