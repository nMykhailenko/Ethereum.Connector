namespace Ethereum.Connector.Application.MaterialManufacturing.Models.ResponseModels
{
    /// <summary>
    /// Record describes material manufacturing response model.
    /// </summary>
    public record MaterialManufacturingResponseModel
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <param name="name">Name.</param>
        public MaterialManufacturingResponseModel(long id, string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// Gets or sets an id.
        /// </summary>
        public long Id { get; init; }
        
        /// <summary>
        /// Gets or sets a name.
        /// </summary>
        public string Name { get; init; }
    }
}