using Ethereum.Connector.Application.MaterialManufacturing.Commands;
using Ethereum.Connector.Application.MaterialManufacturing.Validators;
using FluentAssertions;
using Xunit;

namespace Ethereum.Connector.Application.UnitTests.MaterialManufacturing
{
    public class MaterialManufacturingValidatorTests
    {
        [Fact]
        public void CreateMaterialManufacturingValidator_ShouldReturnTrue_If_ModelIsValid()
        {
            // arrange
            var command = new CreateMaterialManufacturingCommand {Name = "Test Material"};
            var validator = new CreateMaterialManufacturingCommandValidator();
            
            // act
            var result = validator.Validate(command);
            
            // assert
            result.IsValid.Should().Be(true);
        }
    }
}