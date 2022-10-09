using SonarWave.Core.Interfaces;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SonarWave.Application.DependencyInjection
{
    /// <summary>
    /// Used for registring services.
    /// </summary>
    public class ServiceRegistrant : IServiceRegistrant
    {
        public void Register(IServiceCollection services, IConfiguration _)
        {
            services.AddAutoMapper(typeof(MapperInitializer));

            services.AddSignalR(opt =>
            {
                opt.EnableDetailedErrors = true;
                opt.ClientTimeoutInterval = TimeSpan.FromMinutes(10);
                opt.MaximumReceiveMessageSize = 1000000000;
            })
                .AddJsonProtocol(options =>
                {
                    options.PayloadSerializerOptions.Converters
                       .Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
                });
            //.AddMessagePackProtocol(opt =>
            //{
            //    opt.SerializerOptions = MessagePackSerializerOptions
            //        .Standard
            //        .WithResolver(StandardResolver.Instance)
            //        .WithResolver(DynamicEnumAsStringResolver.Instance)
            //        .WithSecurity(MessagePackSecurity.UntrustedData);
            //});
        }
    }
}