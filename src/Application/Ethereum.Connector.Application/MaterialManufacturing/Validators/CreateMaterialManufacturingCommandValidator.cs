using Ethereum.Connector.Application.MaterialManufacturing.Commands;
using FluentValidation;

namespace Ethereum.Connector.Application.MaterialManufacturing.Validators
{
    /// <summary>
    /// Create material manufacturing command validator.
    /// </summary>
    public class CreateMaterialManufacturingCommandValidator 
        : AbstractValidator<CreateMaterialManufacturingCommand>
    {
        /// <summary>
        /// Constructor with validation rules.
        /// </summary>
        public CreateMaterialManufacturingCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
        }
    }
}