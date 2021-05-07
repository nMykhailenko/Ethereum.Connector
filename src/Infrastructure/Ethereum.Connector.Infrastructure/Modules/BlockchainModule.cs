using Ethereum.Connector.Application.Common.Interfaces.Configuration;
using Ethereum.Connector.Application.Common.Interfaces.Ethereum;
using Ethereum.Connector.Application.Common.Options;
using Ethereum.Connector.Infrastructure.Services.Ethereum;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Ethereum.Connector.Infrastructure.Modules
{
    public class BlockchainModule : IInjectModule
    {
        private readonly IConfiguration _configuration;

        public BlockchainModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public IServiceCollection Load(IServiceCollection services)
        {
            services.Configure<AzureBlockchainServiceOptions>(
                x => _configuration
                    .GetSection(nameof(AzureBlockchainServiceOptions)).Bind(x));

            services.AddScoped(typeof(IEthereumService<>), typeof(EthereumService<>));
            
            return services;
        }
    }
}