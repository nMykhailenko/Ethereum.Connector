using System.Threading;
using System.Threading.Tasks;
using Ethereum.Connector.Domain.Entities;

namespace Ethereum.Connector.Application.Common.Interfaces.Database
{
    /// <summary>
    /// Blockchain repository interface.
    /// </summary>
    public interface IBlockchainRepository
    {
        // Smart-Contract actions.
        
        /// <summary>
        /// Get smart-contract by type async.
        /// </summary>
        /// <param name="contractType">Contract type.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Smart contract entity or null.</returns>
        Task<SmartContract> GetSmartContractByTypeAsync(string contractType, CancellationToken cancellationToken);

        // Deployed Smart-Contract actions.
        
        /// <summary>
        /// Add deployed smart-contract async.
        /// </summary>
        /// <param name="entity">Deployed smart-contract entity.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Deployed smart-contract entity.</returns>
        Task<DeployedSmartContract> AddDeployedSmartContractAsync(DeployedSmartContract entity, CancellationToken cancellationToken);

        /// <summary>
        /// Get deployed smart-contract by id async.
        /// </summary>
        /// <param name="id">The id of smart-contract.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Deployed smart-contract entity or null.</returns>
        Task<DeployedSmartContract> GetDeployedSmartContractByIdAsync(long id, CancellationToken cancellationToken);
    }
}