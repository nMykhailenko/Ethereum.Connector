using Ethereum.Connector.Application.Common.Interfaces.Base;

namespace Ethereum.Connector.Application.MaterialManufacturing.Commands
{
    public record CreateMaterialManufacturingCommand : ICommand
    {
        public CreateMaterialManufacturingCommand()
        {
            
        }

        public string Name { get; init; }
    }
}