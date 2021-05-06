using Ethereum.Connector.Application.Common.Interfaces.Database;
using Ethereum.Connector.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ethereum.Connector.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<SmartContract> SmartContracts { get; set; }
        public DbSet<DeployedSmartContract> DeployedSmartContracts { get; set; }
    }
}