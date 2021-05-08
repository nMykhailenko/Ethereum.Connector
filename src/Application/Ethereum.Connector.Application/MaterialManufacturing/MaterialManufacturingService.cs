using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Ethereum.Connector.Application.Common.Interfaces.Database;
using Ethereum.Connector.Application.Common.Interfaces.Ethereum;
using Ethereum.Connector.Application.MaterialManufacturing.Commands;
using Ethereum.Connector.Application.MaterialManufacturing.Contract;
using Ethereum.Connector.Application.MaterialManufacturing.Models;
using Ethereum.Connector.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ethereum.Connector.Application.MaterialManufacturing
{
    public class MaterialManufacturingService: IMaterialManufacturingService
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

        public async Task CreateMaterialManufacturingAsync(
            CreateMaterialManufacturingCommand command, 
            CancellationToken cancellationToken)
        {
            var deploymentModel = _mapper.Map<MaterialManufacturingDeployment>(command);

            var smartContract = await _blockchainRepository.GetSmartContractByTypeAsync(ContractType, cancellationToken);
            
            // TODO use OneOf nuget for returning value.
            if (smartContract == null) throw new Exception($"Smart-contract with type: {ContractType} not found");
            
            var deployedContractAddress = await _ethereumService.DeployAsync(deploymentModel, smartContract);
            var deployedSmartContractModel = new DeployedSmartContract(
                deployedContractAddress,
                ContractType,
                smartContract.Abi);

            await _blockchainRepository.AddDeployedSmartContractAsync(deployedSmartContractModel, cancellationToken);
        }
    }
}