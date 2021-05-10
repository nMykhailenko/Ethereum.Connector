using Ethereum.Connector.Application.MaterialManufacturing.Commands;
using FluentValidation;

namespace Ethereum.Connector.Application.MaterialManufacturing.Validators
{
    public class CreateMaterialManufacturingCommandValidator 
        : AbstractValidator<CreateMaterialManufacturingCommand>
    {
        public CreateMaterialManufacturingCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
        }
    }
}