using Ethereum.Connector.Application.Common.Interfaces.Configuration;
using Ethereum.Connector.Application.MaterialManufacturing.Commands;
using Ethereum.Connector.Application.MaterialManufacturing.Contract;
using Ethereum.Connector.Application.MaterialManufacturing.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Ethereum.Connector.Application.MaterialManufacturing.Module
{
    /// <summary>
    /// Material manufacturing module.
    /// </summary>
    public class MaterialManufacturingModule : IInjectModule
    {
        /// <inheritdoc />
        public IServiceCollection Load(IServiceCollection services)
        {
            services.AddScoped<IMaterialManufacturingService, MaterialManufacturingService>();

            services
                .AddTransient<IValidator<CreateMaterialManufacturingCommand>,
                    CreateMaterialManufacturingCommandValidator>();

            return services;
        }
    }
}