using System;
using System.Threading;
using System.Threading.Tasks;
using Ethereum.Connector.Application.Common.ErrorModels;
using Ethereum.Connector.Application.Common.ErrorModels.ResponseModels;
using Ethereum.Connector.Application.MaterialManufacturing.Commands;
using Ethereum.Connector.Application.MaterialManufacturing.Contract;
using Ethereum.Connector.Application.MaterialManufacturing.Models.ResponseModels;
using Ethereum.Connector.Application.MaterialManufacturing.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ethereum.Connector.API.Controllers
{
    /// <summary>
    /// Material manufacturing controller.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/material-manufacturing")]
    public class MaterialManufacturingController : Controller
    {
        private readonly IMaterialManufacturingService _materialManufacturingService;
        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="materialManufacturingService">Material manufacturing service.</param>
        public MaterialManufacturingController(IMaterialManufacturingService materialManufacturingService)
        {
            _materialManufacturingService = materialManufacturingService
                ?? throw  new ArgumentNullException(nameof(materialManufacturingService));
        }

        /// <summary>
        /// Get material manufacturing by Id.
        /// </summary>
        /// <param name="id">The id of smart-contract.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The instance of material manufacturing smart-contract.</returns>
        [HttpGet("id")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MaterialManufacturingResponseModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseModel))]   
        public async Task<IActionResult> GetMaterialManufacturing(
            [FromRoute]long id, 
            CancellationToken cancellationToken)
        {
            var result = await _materialManufacturingService
                .GetMaterialManufacturingByIdAsync(id, cancellationToken);
            
            return result.Match<IActionResult>(
                Ok,
                notFound => NotFound(new ErrorResponseModel(notFound.Message, nameof(EntityNotFound))));
        }
        
        /// <summary>
        /// Create material manufacturing smart-contract in blockchain network.
        /// </summary>
        /// <param name="command">Create material manufacturing command.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns></returns>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MaterialManufacturingResponseModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseModel))]
        public async Task<IActionResult> AddMaterialManufacturing(
            [FromBody] CreateMaterialManufacturingCommand command,
            CancellationToken cancellationToken)
        {
            var validationResult = await new CreateMaterialManufacturingCommandValidator()
                .ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                var error = new ErrorResponseModel(
                    validationResult.ToString(","), 
                    nameof(EntityNotValid));
                return BadRequest(error);
            }

            var result = await _materialManufacturingService
                .CreateMaterialManufacturingAsync(command, cancellationToken);

            return result.Match<IActionResult>(
                entity
                    => Created($"api/material-manufacturing/{entity.Id}", entity),
                notFound => BadRequest(new ErrorResponseModel(notFound.Message, nameof(EntityNotFound)))
            );
        }
    }
}