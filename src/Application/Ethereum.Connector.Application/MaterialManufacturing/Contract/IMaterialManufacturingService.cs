using System.Threading;
using System.Threading.Tasks;
using Ethereum.Connector.Application.Common.ErrorModels;
using Ethereum.Connector.Application.MaterialManufacturing.Commands;
using Ethereum.Connector.Application.MaterialManufacturing.Models.ResponseModels;
using OneOf;

namespace Ethereum.Connector.Application.MaterialManufacturing.Contract
{
    /// <summary>
    /// Material manufacturing service.
    /// </summary>
    public interface IMaterialManufacturingService
    {
        /// <summary>
        /// Create material manufacturing async.
        /// </summary>
        /// <param name="command">Create material manufacturing command.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns></returns>
        Task<OneOf<MaterialManufacturingResponseModel, EntityNotFound>> CreateMaterialManufacturingAsync(
            CreateMaterialManufacturingCommand command, 
            CancellationToken cancellationToken);

        /// <summary>
        /// Get material manufacturing by id async.
        /// </summary>
        /// <param name="id">Material manufacturing id.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns></returns>
        Task<OneOf<MaterialManufacturingResponseModel, EntityNotFound>> GetMaterialManufacturingByIdAsync(
            long id,
            CancellationToken cancellationToken);
    }
}