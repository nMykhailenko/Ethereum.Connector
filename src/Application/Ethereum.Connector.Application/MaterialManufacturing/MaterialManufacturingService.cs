using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using OneOf;
using Ethereum.Connector.Application.Common.ErrorModels;
using Ethereum.Connector.Application.Common.Interfaces.Database;
using Ethereum.Connector.Application.Common.Interfaces.Ethereum;
using Ethereum.Connector.Application.MaterialManufacturing.Commands;
using Ethereum.Connector.Application.MaterialManufacturing.Contract;
using Ethereum.Connector.Application.MaterialManufacturing.Models;
using Ethereum.Connector.Application.MaterialManufacturing.Models.ResponseModels;
using Ethereum.Connector.Domain.Entities;

namespace Ethereum.Connector.Application.MaterialManufacturing
{
    /// <summary>
    /// Instance of Material manufacturing service.
    /// </summary>
    public class MaterialManufacturingService : IMaterialManufacturingService
    {
        private const string ContractType = "MaterialManufacturing";
        
        private readonly IBlockchainRepository _blockchainRepository;
        private readonly IEthereumService<MaterialManufacturingDeployment> _ethereumService;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="blockchainRepository">Blockchain repository.</param>
        /// <param name="ethereumService">Ethereum service.</param>
        public MaterialManufacturingService(
            IBlockchainRepository blockchainRepository, 
            IEthereumService<MaterialManufacturingDeployment> ethereumService)
        {
            _blockchainRepository = blockchainRepository ?? throw new ArgumentNullException(nameof(blockchainRepository));
            _ethereumService = ethereumService ?? throw new ArgumentNullException(nameof(ethereumService));
        }
        
        /// <inheritdoc />
        public async Task<OneOf<MaterialManufacturingResponseModel, EntityNotFound>> GetMaterialManufacturingByIdAsync(long id, CancellationToken cancellationToken)
        {
            var smartContract = await _blockchainRepository
                .GetDeployedSmartContractByIdAsync(id, cancellationToken);

            if (smartContract is null)
            {
                return new EntityNotFound {Message = $"The deployed smart-contract with ID: {id} not found."};
            }

            // TODO need to add getting from blockchain network.
            return new MaterialManufacturingResponseModel(id, "");
        }
        
        /// <inheritdoc />
        public async Task<OneOf<MaterialManufacturingResponseModel, EntityNotFound>> CreateMaterialManufacturingAsync(
            CreateMaterialManufacturingCommand command, 
            CancellationToken cancellationToken)
        {
            var smartContract =
                await _blockchainRepository.GetSmartContractByTypeAsync(ContractType, cancellationToken);
            if (smartContract == null)
            {
                return new EntityNotFound {Message = $"Smart-contract with type: {ContractType} not found"};
            }
            
            var deployedContractAddress = await _ethereumService.DeployAsync(command, smartContract);
            
            var deployedSmartContractModel = new DeployedSmartContract(
                deployedContractAddress,
                ContractType,
                smartContract.Abi);

            var deployedSmartContract = await _blockchainRepository.AddDeployedSmartContractAsync(deployedSmartContractModel, cancellationToken);

            return new MaterialManufacturingResponseModel(deployedSmartContract.Id, command.Name);
        }
    }
}