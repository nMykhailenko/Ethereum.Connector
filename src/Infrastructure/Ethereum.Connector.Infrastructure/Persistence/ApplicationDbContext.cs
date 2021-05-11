using Ethereum.Connector.Application.Common.Interfaces.Database;
using Ethereum.Connector.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ethereum.Connector.Infrastructure.Persistence
{
    /// <summary>
    /// Application database context instance.
    /// </summary>
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="options">Database context options.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        /// <inheritdoc />
        public DbSet<SmartContract> SmartContracts { get; set; }
        
        /// <inheritdoc />
        public DbSet<DeployedSmartContract> DeployedSmartContracts { get; set; }
    }
}