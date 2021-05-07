using AutoMapper;
using Ethereum.Connector.Application.MaterialManufacturing;
using Ethereum.Connector.Application.MaterialManufacturing.Commands;

namespace Ethereum.Connector.Application.Common.Mappings.Profiles
{
    public class BlockchainProfile : Profile
    {
        public BlockchainProfile()
        {
            CreateMap<CreateMaterialManufacturingCommand, MaterialManufacturingDeployment>();
        }
    }
}