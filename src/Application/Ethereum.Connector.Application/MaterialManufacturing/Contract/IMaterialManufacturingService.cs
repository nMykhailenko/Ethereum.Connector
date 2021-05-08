using System.Threading;
using System.Threading.Tasks;
using Ethereum.Connector.Application.MaterialManufacturing.Commands;

namespace Ethereum.Connector.Application.MaterialManufacturing.Contract
{
    public interface IMaterialManufacturingService
    {
        Task CreateMaterialManufacturingAsync(
            CreateMaterialManufacturingCommand command, 
            CancellationToken cancellationToken);
    }
}