using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SonarWave.Core.Interfaces;
using SonarWave.Infrastructure.Data;
using SonarWave.Infrastructure.Services;

namespace SonarWave.Infrastructure.DependencyInjection
{
    /// <summary>
    /// Used for registring services.
    /// </summary>
    public class ServiceRegistrant : IServiceRegistrant
    {
        public void Register(IServiceCollection services, IConfiguration _)
        {
            services.AddDbContext<DatabaseContext>(opt =>
            {
                opt.UseInMemoryDatabase(nameof(DatabaseContext));
            });

            services.AddTransient<IUserService, UserService>();
        }
    }
}