using Nethereum.Contracts;

namespace Ethereum.Connector.Application.MaterialManufacturing.Models
{
    public class MaterialManufacturingDeployment: ContractDeploymentMessage
    {
        public MaterialManufacturingDeployment() : base(string.Empty)
        {

        }
        public MaterialManufacturingDeployment(string byteCode) : base(byteCode)
        {

        }

    }
}