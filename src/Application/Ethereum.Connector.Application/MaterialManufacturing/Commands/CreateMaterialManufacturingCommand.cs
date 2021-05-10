using Ethereum.Connector.Application.Common.Interfaces.Base;

namespace Ethereum.Connector.Application.MaterialManufacturing.Commands
{
    /// <summary>
    /// Record describes create material manufacturing command.
    /// </summary>
    public record CreateMaterialManufacturingCommand : ICommand
    {
        public CreateMaterialManufacturingCommand()
        {
            
        }

        public string Name { get; init; }
    }
}