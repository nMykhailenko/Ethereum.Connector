using System.Threading;
using System.Threading.Tasks;
using Ethereum.Connector.Application.Common.ErrorModels;
using Ethereum.Connector.Application.MaterialManufacturing.Commands;
using Ethereum.Connector.Application.MaterialManufacturing.Models.ResponseModels;
using OneOf;

namespace Ethereum.Connector.Application.MaterialManufacturing.Contract
{
    public interface IMaterialManufacturingService
    {
        Task<OneOf<MaterialManufacturingResponseModel, EntityNotFound>> CreateMaterialManufacturingAsync(
            CreateMaterialManufacturingCommand command, 
            CancellationToken cancellationToken);
    }
}