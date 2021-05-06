using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Ethereum.Connector.Application.Common.Extensions;
using Ethereum.Connector.Application.Common.Interfaces.Ethereum;
using Ethereum.Connector.Application.Common.Options;
using Ethereum.Connector.Domain.Entities;
using Ethereum.Connector.Infrastructure.Persistence;
using Microsoft.Extensions.Options;
using Nethereum.ABI.Model;
using Nethereum.Contracts;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

namespace Ethereum.Connector.Infrastructure.Services.Ethereum
{
    public class EthereumService<TContractDeployment> : IEthereumService<TContractDeployment>
        where TContractDeployment : ContractDeploymentMessage, new()
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly AzureBlockchainServiceOptions _blockchainServiceOptions;

        public EthereumService(
            IMapper mapper,
            ApplicationDbContext context,
            IOptions<AzureBlockchainServiceOptions> blockchainServiceOptions)
        {
            _mapper = mapper;
            _context = context;
            _blockchainServiceOptions = blockchainServiceOptions.Value;
        }

        public async Task<string> DeployAsync<TModelDto>(TModelDto modelDto, SmartContract smartContract)
            where TModelDto : class
        {
            var account = new Account(_blockchainServiceOptions.AccountAddress);
            var web3 = new Web3(account, _blockchainServiceOptions.BlockchainRpcEndpoint);

            await web3.Personal.UnlockAccount
                .SendRequestAsync(
                    _blockchainServiceOptions.AccountAddress,
                    _blockchainServiceOptions.AccountPassword,
                    120);

            var contractDeployment = _mapper.Map<TContractDeployment>((modelDto, smartContract.ByteCode));
            var deploymentHandler = web3.Eth.GetContractDeploymentHandler<TContractDeployment>();
            var deploymentReceipt = await deploymentHandler.SendRequestAndWaitForReceiptAsync(contractDeployment);

            return deploymentReceipt.ContractAddress;
        }

        public async Task<TOutput> QueryAsync<TFunction, TOutput>(string contractAddress)
            where TFunction : FunctionMessage, new()
        {
            var account = new Account(_blockchainServiceOptions.AccountAddress);
            var web3 = new Web3(account, _blockchainServiceOptions.BlockchainRpcEndpoint);

            await web3.Personal.UnlockAccount
                .SendRequestAsync(
                    _blockchainServiceOptions.AccountAddress,
                    _blockchainServiceOptions.AccountPassword,
                    120);

            var contractHandler = web3.Eth.GetContractHandler(contractAddress);
            var result = await contractHandler.QueryAsync<TFunction, TOutput>();

            return result;
        }

        public async Task CommandAsync<TInput>(TInput body, int contractId, string functionName)
        {
            var contractEntity = await _context.DeployedSmartContracts.FindAsync(contractId);
            if (contractEntity == null)
                throw new ArgumentNullException($"Entity with ID {contractId} not found.");

            await CommandAsync(body, contractEntity, functionName);
        }

        public async Task CommandAsync<TInput>(
            TInput body,
            DeployedSmartContract contractEntity,
            string functionName)
        {
            var web3 = new Web3(_blockchainServiceOptions.BlockchainRpcEndpoint);
            var contract = web3.Eth.GetContract(contractEntity.Abi, contractEntity.ContractAddress);
            await web3.Personal.UnlockAccount.SendRequestAsync(_blockchainServiceOptions.AccountAddress,
                _blockchainServiceOptions.AccountPassword, 120);

            var functionAbi = contract.ContractBuilder.ContractABI.Functions
                .FirstOrDefault(f => f.Name == functionName);
            if (functionAbi == null)
                throw new ArgumentNullException($"{functionName} for contract not found.");

            var functionParameters = functionAbi.InputParameters ?? new Parameter[] { };
            var bodyProperties = body.GetPropertiesInfo();

            if (!ValidateModelParameters(bodyProperties, functionParameters))
                throw new ArgumentNullException($"Parameters do not match.");

            var arguments = functionParameters.GetArguments(body);
            var function = contract.GetFunction(functionName);

            var estimated = await web3.TransactionManager
                .EstimateGasAsync(function.CreateCallInput(arguments));
            var transactionInput = function
                .CreateTransactionInput(_blockchainServiceOptions.AccountAddress, arguments);

            web3.TransactionManager.DefaultGas = estimated.Value;
            web3.TransactionManager.DefaultGasPrice = 0;

            var transactionRseceipt = await web3.TransactionManager
                .SendTransactionAndWaitForReceiptAsync(transactionInput, null);
        }

        private bool ValidateModelParameters(IEnumerable<PropertyInfo> properties, IEnumerable<Parameter> parameters)
            => parameters
                .Select(
                    parameter => properties.FirstOrDefault(
                        x => string.Equals(
                            x.Name, 
                            parameter.Name, 
                            StringComparison.InvariantCultureIgnoreCase)))
                .All(property => property != null);
    }
}