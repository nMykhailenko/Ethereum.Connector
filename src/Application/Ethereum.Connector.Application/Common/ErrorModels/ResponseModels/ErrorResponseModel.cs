namespace Ethereum.Connector.Application.Common.ErrorModels.ResponseModels
{
    /// <summary>
    /// Class describes error response model.
    /// </summary>
    public record ErrorResponseModel
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="code">The error code.</param>
        public ErrorResponseModel(string message, string code)
        {
            Message = message;
            Code = code;
        }
        
        /// <summary>
        /// Gets or sets an error message.
        /// </summary>
        public string Message { get; init; }
        
        /// <summary>
        /// Gets or sets an error code.
        /// </summary>
        public string Code { get; init; }
    }
}