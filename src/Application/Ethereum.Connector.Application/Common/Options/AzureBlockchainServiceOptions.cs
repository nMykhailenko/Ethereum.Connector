namespace Ethereum.Connector.Application.Common.Options
{
    public class AzureBlockchainServiceOptions
    {
        /// <summary>
        /// Gets or sets the blockchain RPC endpoint.
        /// </summary>
        public string BlockchainRpcEndpoint { get; set; }
        
        /// <summary>
        /// Gets or sets the account address.
        /// </summary>
        public string AccountAddress { get; set; }
        
        /// <summary>
        /// Gets or sets the account password.
        /// </summary>
        public string AccountPassword { get; set; }
    }
}