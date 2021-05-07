using Microsoft.Extensions.DependencyInjection;

namespace Ethereum.Connector.Application.Common.Interfaces.Configuration
{
    /// <summary>
    /// Inject module.
    /// </summary>
    public interface IInjectModule
    {
        /// <summary>
        /// Load dependecies.
        /// </summary>
        /// <param name="services">Current service collection.</param>
        /// <returns>Updated service collection.</returns>
        IServiceCollection Load(IServiceCollection services);
    }
}