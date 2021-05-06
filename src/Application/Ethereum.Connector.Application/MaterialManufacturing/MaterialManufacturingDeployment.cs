using Nethereum.Contracts;

namespace Ethereum.Connector.Application.MaterialManufacturing
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