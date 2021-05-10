using Nethereum.Contracts;

namespace Ethereum.Connector.Application.MaterialManufacturing.Models
{
    /// <summary>
    /// Class describes material manufacturing deployment.
    /// </summary>
    public class MaterialManufacturingDeployment: ContractDeploymentMessage
    {
        /// <summary>
        /// Base constructor.
        /// </summary>
        public MaterialManufacturingDeployment() : base(string.Empty)
        {

        }
        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="byteCode">Byte code.</param>
        public MaterialManufacturingDeployment(string byteCode) : base(byteCode)
        {

        }

    }
}