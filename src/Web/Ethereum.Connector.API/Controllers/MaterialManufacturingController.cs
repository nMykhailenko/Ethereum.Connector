using System;
using System.Threading;
using System.Threading.Tasks;
using Ethereum.Connector.Application.MaterialManufacturing.Commands;
using Ethereum.Connector.Application.MaterialManufacturing.Contract;
using Microsoft.AspNetCore.Mvc;

namespace Ethereum.Connector.API.Controllers
{
    public class MaterialManufacturingController : Controller
    {
        private readonly IMaterialManufacturingService _materialManufacturingService;

        public MaterialManufacturingController(IMaterialManufacturingService materialManufacturingService)
        {
            _materialManufacturingService = materialManufacturingService
                ?? throw  new ArgumentNullException(nameof(materialManufacturingService));
        }

        /// <summary>
        /// Create material manufacturing smart-contract in blockchain network.
        /// </summary>
        /// <param name="command">Create material manufacturing command.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/material-manufacturing")]
        public async Task<IActionResult> AddMaterialManufacturing(
            [FromBody] CreateMaterialManufacturingCommand command,
            CancellationToken cancellationToken)
        {
            await _materialManufacturingService.CreateMaterialManufacturingAsync(command, cancellationToken);

            return Ok();
        }
    }
}