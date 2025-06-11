using AMAPP.API.Data;
using AMAPP.API.Models;
using AMAPP.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using static AMAPP.API.Constants;

namespace AMAPP.API.Services.Implementations
{
    public class UserRoleInfoService : IUserRoleInfoService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserRoleInfoService> _logger;

        public UserRoleInfoService(ApplicationDbContext context, ILogger<UserRoleInfoService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task CreateRoleInfoAsync(string userId, List<string> roleNames)
        {
            try
            {
                using var transaction = await _context.Database.BeginTransactionAsync();

                foreach (var roleName in roleNames)
                {
                    switch (roleName)
                    {
                        case RoleNames.Producer:
                            await CreateProducerInfoIfNotExistsAsync(userId);
                            break;
                        case RoleNames.CoProducer:
                            await CreateCoproducerInfoIfNotExistsAsync(userId);
                            break;
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                _logger.LogInformation("Successfully created role info for user {UserId} with roles {Roles}",
                    userId, string.Join(", ", roleNames));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating role info for user {UserId}", userId);
                throw;
            }
        }

        public async Task<bool> HasProducerInfoAsync(string userId)
        {
            return await _context.ProducersInfo.AnyAsync(p => p.UserId == userId);
        }

        public async Task<bool> HasCoproducerInfoAsync(string userId)
        {
            return await _context.CoproducersInfo.AnyAsync(c => c.UserId == userId);
        }

        private async Task CreateProducerInfoIfNotExistsAsync(string userId)
        {
            var existingProducerInfo = await _context.ProducersInfo
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (existingProducerInfo == null)
            {
                var producerInfo = new ProducerInfo
                {
                    UserId = userId
                };

                _context.ProducersInfo.Add(producerInfo);
                _logger.LogInformation("Created ProducerInfo for user {UserId}", userId);
            }
            else
            {
                _logger.LogDebug("ProducerInfo already exists for user {UserId}", userId);
            }
        }

        private async Task CreateCoproducerInfoIfNotExistsAsync(string userId)
        {
            var existingCoproducerInfo = await _context.CoproducersInfo
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (existingCoproducerInfo == null)
            {
                var coproducerInfo = new CoproducerInfo
                {
                    UserId = userId
                };

                _context.CoproducersInfo.Add(coproducerInfo);

                // Criar CheckingAccount automaticamente
                var checkingAccount = new CheckingAccount
                {
                    Coproducer = coproducerInfo,
                    Balance = 0.0
                };

                _context.CheckingAccounts.Add(checkingAccount);
                _logger.LogInformation("Created CoproducerInfo and CheckingAccount for user {UserId}", userId);
            }
            else
            {
                _logger.LogDebug("CoproducerInfo already exists for user {UserId}", userId);
            }
        }
    }
}
