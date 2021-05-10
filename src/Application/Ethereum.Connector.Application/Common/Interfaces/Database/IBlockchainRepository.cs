using System.Threading;
using System.Threading.Tasks;
using Ethereum.Connector.Domain.Entities;

namespace Ethereum.Connector.Application.Common.Interfaces.Database
{
    public interface IBlockchainRepository
    {
        // Smart-Contract actions.
        Task<SmartContract> GetSmartContractByTypeAsync(string contractType, CancellationToken cancellationToken);

        // Deployed Smart-Contract actions.
        Task<DeployedSmartContract> AddDeployedSmartContractAsync(DeployedSmartContract entity, CancellationToken cancellationToken);

        Task<DeployedSmartContract> GetDeployedSmartContractByIdAsync(long id, CancellationToken cancellationToken);
    }
}