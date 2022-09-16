using SonarWave.Core.Interfaces;

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

            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
                options.ClientTimeoutInterval = TimeSpan.FromMinutes(10);
                options.MaximumReceiveMessageSize = 1000000000;
            });
        }
    }
}