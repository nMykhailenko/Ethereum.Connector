namespace Ethereum.Connector.Application.Common.ErrorModels.ResponseModels
{
    public record ErrorResponseModel
    {
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