using System.Threading.Tasks;
using Ethereum.Connector.Domain.Entities;
using Nethereum.Contracts;

namespace Ethereum.Connector.Application.Common.Interfaces.Ethereum
{
    /// <summary>
    /// IEthereum service.
    /// </summary>
    /// <typeparam name="TContractDeployment"></typeparam>
    public interface IEthereumService<TContractDeployment> where TContractDeployment
        : ContractDeploymentMessage, new()
    {
        /// <summary>
        /// Deploy smart-contract async.
        /// </summary>
        /// <param name="modelDto">Smart-contract parameters of deploy.</param>
        /// <param name="smartContract">Smart-contract metadata.</param>
        /// <typeparam name="TModelDto"></typeparam>
        /// <returns>Address of deployed smart-contract.</returns>
        Task<string> DeployAsync<TModelDto>(TModelDto modelDto, SmartContract smartContract) where TModelDto : class;
        
        /// <summary>
        /// Query async.
        /// </summary>
        /// <param name="contractAddress">Smart-contract address.</param>
        /// <typeparam name="TFunction">Function to execute from blockchain network.</typeparam>
        /// <typeparam name="TOutput">Response model of result.</typeparam>
        /// <returns></returns>
        Task<TOutput> QueryAsync<TFunction, TOutput>(string contractAddress) where TFunction : FunctionMessage, new();
        
        /// <summary>
        /// Command async
        /// </summary>
        /// <param name="body">Command to execute.</param>
        /// <param name="contractId">Smart-contract id.</param>
        /// <param name="functionName">Function to execute.</param>
        /// <typeparam name="TInput">Command type to execute.</typeparam>
        /// <returns></returns>
        Task CommandAsync<TInput>(TInput body, int contractId, string functionName);
        
        /// <summary>
        /// Command async.
        /// </summary>
        /// <param name="body">Command to execute.</param>
        /// <param name="contractEntity">Smart-contract entity.</param>
        /// <param name="functionName">Function to execute.</param>
        /// <typeparam name="TInput">Command type to execute.</typeparam>
        /// <returns></returns>
        Task CommandAsync<TInput>(TInput body, DeployedSmartContract contractEntity, string functionName);
    }
}