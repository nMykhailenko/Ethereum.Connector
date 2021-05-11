using Ethereum.Connector.Application.Common.Extensions;
using Ethereum.Connector.Application.MaterialManufacturing.Commands;
using FluentAssertions;
using Xunit;

namespace Ethereum.Connector.Application.UnitTests.Common.Extensions
{
    public class ObjectExtensionsUnitTests
    {
        [Fact]
        public void GetPropertiesInfo_ShouldReturn_ObjectPropertiesList()
        {
            // arrange
            var command = new CreateMaterialManufacturingCommand();
            var expectedProperties = command.GetType().GetProperties();
            
            // act
            var result = command.GetPropertiesInfo();
            
            // assert
            result.Should().NotBeNull();
            result.Should().Equal(expectedProperties);
        }
    }
}