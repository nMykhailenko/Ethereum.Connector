using System.Data.SqlTypes;
using Ethereum.Connector.Application.Common.Interfaces.Configuration;
using Ethereum.Connector.Application.Common.Interfaces.Database;
using Ethereum.Connector.Application.Common.Options;
using Ethereum.Connector.Infrastructure.Persistence;
using Ethereum.Connector.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ethereum.Connector.Infrastructure.Modules
{
    /// <summary>
    /// SQL module.
    /// </summary>
    public class SqlModule : IInjectModule
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="configuration">Configuration.</param>
        public SqlModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <inheritdoc />
        public IServiceCollection Load(IServiceCollection services)
        {
            var sqlConnectionOptions = _configuration
                .GetSection(nameof(SqlConnectionOptions)).Get<SqlConnectionOptions>();
            
            services.Configure<SqlConnectionOptions>(
                x => _configuration.GetSection(nameof(SqlCompareOptions)).Bind(x));

            services.AddScoped<IBlockchainRepository, BlockchainRepository>();
            
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(sqlConnectionOptions.DefaultConnection, _ => _.EnableRetryOnFailure()));

            return services;
        }
    }
}