using Ethereum.Connector.Application.Common.Interfaces.Configuration;
using Ethereum.Connector.Application.Common.Mappings.Profiles;
using Microsoft.Extensions.DependencyInjection;

namespace Ethereum.Connector.Application.Common.Mappings.Modules
{
    public class MappingModule : IInjectModule
    {
        public IServiceCollection Load(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(BlockchainProfile));

            return services;
        }
    }
}