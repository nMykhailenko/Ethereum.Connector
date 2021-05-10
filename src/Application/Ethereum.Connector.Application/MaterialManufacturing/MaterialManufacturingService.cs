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
    public class MaterialManufacturingService : IMaterialManufacturingService
    {
        private const string ContractType = "MaterialManufacturing";
        
        private readonly IMapper _mapper;
        private readonly IBlockchainRepository _blockchainRepository;
        private readonly IEthereumService<MaterialManufacturingDeployment> _ethereumService;

        public MaterialManufacturingService(
            IMapper mapper,
            IBlockchainRepository blockchainRepository, 
            IEthereumService<MaterialManufacturingDeployment> ethereumService)
        {
            _blockchainRepository = blockchainRepository ?? throw new ArgumentNullException(nameof(blockchainRepository));
            _ethereumService = ethereumService ?? throw new ArgumentNullException(nameof(ethereumService));
            _mapper = mapper ?? throw  new ArgumentNullException(nameof(mapper));
        }

        public async Task<OneOf<MaterialManufacturingResponseModel, EntityNotFound>> GetMaterialManufacturingAsync(long id, CancellationToken cancellationToken)
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
        
        public async Task<OneOf<MaterialManufacturingResponseModel, EntityNotFound>> CreateMaterialManufacturingAsync(
            CreateMaterialManufacturingCommand command, 
            CancellationToken cancellationToken)
        {
            var deploymentModel = _mapper.Map<MaterialManufacturingDeployment>(command);

            var smartContract = await _blockchainRepository.GetSmartContractByTypeAsync(ContractType, cancellationToken);
            
            if (smartContract == null)
            {
                return new EntityNotFound {Message = $"Smart-contract with type: {ContractType} not found"};
            }
            
            var deployedContractAddress = await _ethereumService.DeployAsync(deploymentModel, smartContract);
            var deployedSmartContractModel = new DeployedSmartContract(
                deployedContractAddress,
                ContractType,
                smartContract.Abi);

            var deployedSmartContract = await _blockchainRepository.AddDeployedSmartContractAsync(deployedSmartContractModel, cancellationToken);

            return new MaterialManufacturingResponseModel(deployedSmartContract.Id, command.Name);
        }
    }
}