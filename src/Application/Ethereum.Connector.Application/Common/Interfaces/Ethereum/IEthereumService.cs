using System.Threading.Tasks;
using Ethereum.Connector.Domain.Entities;
using Nethereum.Contracts;

namespace Ethereum.Connector.Application.Common.Interfaces.Ethereum
{
    public interface IEthereumService<TContractDeployment> where TContractDeployment : ContractDeploymentMessage, new()
    {
        Task<string> DeployAsync<TModelDto>(TModelDto modelDto, SmartContract smartContract) where TModelDto : class;
        Task<TOutput> QueryAsync<TFunction, TOutput>(string contractAddress) where TFunction : FunctionMessage, new();
        Task CommandAsync<TInput>(TInput body, int contractId, string functionName);
        Task CommandAsync<TInput>(TInput body, DeployedSmartContract contractEntity, string functionName);
    }
}