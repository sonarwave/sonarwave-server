using SonarWave.Infrastructure.Data;

namespace SonarWave.Infrastructure.Services
{
    /// <summary>
    /// Represents a unique abstract service that holds common functionalities,
    /// between different services.
    /// </summary>
    public abstract class AbstractService : IAsyncDisposable
    {
        protected readonly DatabaseContext _context;

        protected AbstractService(DatabaseContext context)
        {
            _context = context;
        }

        public ValueTask DisposeAsync()
        {
            GC.SuppressFinalize(this);
            return _context.DisposeAsync();
        }
    }
}