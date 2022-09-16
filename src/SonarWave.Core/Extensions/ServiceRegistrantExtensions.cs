using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SonarWave.Core.Interfaces;
using System.Reflection;

namespace SonarWave.Core.Extensions
{
    /// <summary>
    /// Extensions for <see cref="IServiceRegistrant"/>.
    /// </summary>
    public static class ServiceRegistrantExtensions
    {
        /// <summary>
        /// An extension for registring services in assembly.
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterServicesFromAssemblies(this IServiceCollection services, IConfiguration configuration)
        {
            var entryAssembly = Assembly.GetEntryAssembly();
            var assemblies = new List<Assembly>();

            if (entryAssembly != null)
            {
                assemblies = entryAssembly
                     .GetReferencedAssemblies()
                     .Distinct()
                     .Select(opt => Assembly.Load(opt))
                     .ToList();

                assemblies.Add(entryAssembly);
            }

            foreach (var assembly in assemblies)
            {
                var registrants = assembly.ExportedTypes.Where(x => typeof(IServiceRegistrant).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).Select(Activator.CreateInstance).Cast<IServiceRegistrant>().ToList();

                registrants.ForEach(x => x.Register(services, configuration));
            }
        }
    }
}