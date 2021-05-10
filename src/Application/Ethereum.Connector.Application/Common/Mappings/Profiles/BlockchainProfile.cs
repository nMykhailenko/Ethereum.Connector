using AutoMapper;
using Ethereum.Connector.Application.MaterialManufacturing.Commands;
using Ethereum.Connector.Application.MaterialManufacturing.Models;

namespace Ethereum.Connector.Application.Common.Mappings.Profiles
{
    /// <summary>
    /// Blockchain profile.
    /// </summary>
    public class BlockchainProfile : Profile
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public BlockchainProfile()
        {
            CreateMap<CreateMaterialManufacturingCommand, MaterialManufacturingDeployment>();
        }
    }
}