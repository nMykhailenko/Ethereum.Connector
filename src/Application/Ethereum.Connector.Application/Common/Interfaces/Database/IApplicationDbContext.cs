using System.Threading;
using System.Threading.Tasks;
using Ethereum.Connector.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ethereum.Connector.Application.Common.Interfaces.Database
{
    public interface IApplicationDbContext
    {
        DbSet<SmartContract> SmartContracts { get; set; } 
        DbSet<DeployedSmartContract> DeployedSmartContracts { get; set; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}