namespace Ethereum.Connector.Domain.Entities
{
    /// <summary>
    /// Class represents deployed smart-contract.
    /// </summary>
    public class DeployedSmartContract
    {
        public DeployedSmartContract()
        {
            
        }

        public DeployedSmartContract(string contractAddress, string type, string abi)
        {
            ContractAddress = contractAddress;
            Type = type;
            Abi = abi;
        }
        
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// Gets or sets your contract type.
        /// </summary>
        public string Type { get; set; }
        
        /// <summary>
        /// Gets or sets the smart-contract address.
        /// </summary>
        public string ContractAddress { get; set; }
        
        /// <summary>
        /// Gets or sets the ABI of smart-contract.
        /// </summary>
        public string Abi { get; set; }
    }
}