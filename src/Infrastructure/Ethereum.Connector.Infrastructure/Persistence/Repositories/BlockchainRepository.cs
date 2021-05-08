using System;
using System.Threading;
using System.Threading.Tasks;
using Ethereum.Connector.Application.Common.Interfaces.Database;
using Ethereum.Connector.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ethereum.Connector.Infrastructure.Persistence.Repositories
{
    public class BlockchainRepository : IBlockchainRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BlockchainRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<SmartContract> GetSmartContractByTypeAsync(string contractType, CancellationToken cancellationToken)
            => _dbContext.SmartContracts
                .FirstOrDefaultAsync(x => string.Equals(
                    x.Type,
                    contractType,
                    StringComparison.InvariantCultureIgnoreCase), cancellationToken);

        public async Task AddDeployedSmartContractAsync(DeployedSmartContract entity, CancellationToken cancellationToken)
        {
            await _dbContext.DeployedSmartContracts.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}