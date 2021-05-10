namespace Ethereum.Connector.Application.MaterialManufacturing.Models.ResponseModels
{
    public record MaterialManufacturingResponseModel
    {
        public MaterialManufacturingResponseModel(long id, string name)
        {
            Id = id;
            Name = name;
        }

        public long Id { get; set; }
        public string Name { get; init; }
    }
}