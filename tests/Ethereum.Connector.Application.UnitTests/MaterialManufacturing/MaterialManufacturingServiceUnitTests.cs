using System;
using System.Threading;
using System.Threading.Tasks;
using Ethereum.Connector.Application.Common.ErrorModels;
using Ethereum.Connector.Application.Common.Interfaces.Database;
using Ethereum.Connector.Application.Common.Interfaces.Ethereum;
using Ethereum.Connector.Application.MaterialManufacturing;
using Ethereum.Connector.Application.MaterialManufacturing.Commands;
using Ethereum.Connector.Application.MaterialManufacturing.Models;
using Ethereum.Connector.Application.MaterialManufacturing.Models.ResponseModels;
using Ethereum.Connector.Domain.Entities;
using FluentAssertions;
using Moq;
using OneOf;
using Xunit;

namespace Ethereum.Connector.Application.UnitTests.MaterialManufacturing
{
    public class MaterialManufacturingServiceUnitTests
    {
        private const string ContractType = "MaterialManufacturing";
        
        [Fact]
        public void MaterialManufacturingServiceConstructor_ShouldThrow_ArgumentNullException_If_BlockchainRepository_IsNull()
        {
            // arrange
            var expectedException = new ArgumentNullException($"blockchainRepository");
            Action action = () => new MaterialManufacturingService(null, null);

            // act & assert
            action.Should().Throw<ArgumentNullException>().WithMessage(expectedException.Message);
        }
        
        [Fact]
        public void MaterialManufacturingServiceConstructor_ShouldThrow_ArgumentNullException_If_EthereumService_IsNull()
        {
            // arrange
            var expectedException = new ArgumentNullException($"ethereumService");
            var blockchainRepositoryMock = Mock.Of<IBlockchainRepository>();
            Action action = () 
                => new MaterialManufacturingService(blockchainRepositoryMock, null);

            // act
            var ex = Assert.Throws<ArgumentNullException>(
                () => new MaterialManufacturingService(blockchainRepositoryMock, null));
            
            // assert
            action.Should().Throw<ArgumentNullException>().WithMessage(expectedException.Message);
        }
        
        [Fact]
        public async Task CreateMaterialManufacturingAsync_ShouldReturn_EntityNotFound_When_SmartContractNotFound()
        {
            // arrange
            var command = new CreateMaterialManufacturingCommand();
            var blockchainRepositoryMock = new Mock<IBlockchainRepository>();
            blockchainRepositoryMock.Setup(
                    x => x.GetSmartContractByTypeAsync(
                        It.IsAny<string>(),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync((SmartContract) null);
            
            var ethereumServiceMock = Mock.Of<IEthereumService<MaterialManufacturingDeployment>>();
            var service = new MaterialManufacturingService(
                blockchainRepositoryMock.Object, 
                ethereumServiceMock);

            // act
            var result = await service
                .CreateMaterialManufacturingAsync(command, CancellationToken.None);

            // assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OneOf<MaterialManufacturingResponseModel, EntityNotFound>>();
            result.IsT1.Should().BeTrue();
        } 
        
        [Fact]
        public async Task CreateMaterialManufacturingAsync_ShouldReturn_CreatedEntity_When_Ok()
        {
            // arrange
            var command = new CreateMaterialManufacturingCommand() { Name = "test name"};
            var contractAddress = "contract address";
            var smartContract = new SmartContract
            {
                Id = 1,
                Abi = "abi",
                ByteCode = "byte code",
                ApiVersion = "v1",
                Type = ContractType
            };
            var deployedSmartContract = new DeployedSmartContract(
                contractAddress,
                ContractType,
                smartContract.Abi) { Id =  1 };
            var expectedResult = new MaterialManufacturingResponseModel(deployedSmartContract.Id, command.Name);
            
            var blockchainRepositoryMock = new Mock<IBlockchainRepository>();
            blockchainRepositoryMock.Setup(
                    x => x.GetSmartContractByTypeAsync(
                        It.IsAny<string>(),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(smartContract);
            blockchainRepositoryMock.Setup(
                    x => x.AddDeployedSmartContractAsync(
                        It.IsAny<DeployedSmartContract>(),
                        CancellationToken.None))
                .ReturnsAsync(deployedSmartContract);
            
            var ethereumServiceMock = new Mock<IEthereumService<MaterialManufacturingDeployment>>();
            ethereumServiceMock.Setup(
                    x => x.DeployAsync(
                        It.IsAny<CreateMaterialManufacturingCommand>(), 
                        It.IsAny<SmartContract>()))
                .ReturnsAsync(contractAddress);
            
            var service = new MaterialManufacturingService(
                blockchainRepositoryMock.Object, 
                ethereumServiceMock.Object);

            // act
            var result = await service
                .CreateMaterialManufacturingAsync(command, CancellationToken.None);

            // assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OneOf<MaterialManufacturingResponseModel, EntityNotFound>>();
            result.IsT0.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(expectedResult);
        } 
    }
}