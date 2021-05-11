namespace Ethereum.Connector.Domain.Entities
{
    /// <summary>
    /// Class represents the Ethereum smart-contract.
    /// </summary>
    public class SmartContract
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// Gets or sets the ABI of smart-contract.
        /// </summary>
        public string Abi { get; set; }
        
        /// <summary>
        /// Gets or sets the byte code of smart-contract.
        /// </summary>
        public string ByteCode { get; set; }
        
        /// <summary>
        /// Gets or sets the smart-contract type.
        /// </summary>
        public string Type { get; set; }
        
        /// <summary>
        /// Gets or sets the API version.
        /// </summary>
        public string ApiVersion { get; set; }
    }
}