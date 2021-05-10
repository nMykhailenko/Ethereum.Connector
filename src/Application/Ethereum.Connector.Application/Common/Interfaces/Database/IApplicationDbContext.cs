using System.Threading;
using System.Threading.Tasks;
using Ethereum.Connector.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ethereum.Connector.Application.Common.Interfaces.Database
{
    /// <summary>
    /// Application database context interface.
    /// </summary>
    public interface IApplicationDbContext
    {
        /// <summary>
        /// Gets or sets a smart contracts.
        /// </summary>
        DbSet<SmartContract> SmartContracts { get; set; } 
        
        /// <summary>
        /// Gets or sets a deployed smart contracts.
        /// </summary>
        DbSet<DeployedSmartContract> DeployedSmartContracts { get; set; }
        
        /// <summary>
        /// Save changes async.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}