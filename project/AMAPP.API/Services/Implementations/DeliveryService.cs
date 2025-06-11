using AMAPP.API.Data;
using AMAPP.API.DTOs.Delivery;
using AMAPP.API.Models;
using AMAPP.API.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AMAPP.API.Services.Implementations
{
    public class DeliveryService : IDeliveryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public DeliveryService(ApplicationDbContext context, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IEnumerable<DeliveryDto>> GetAllAsync()
        {
            var deliveries = await _context.Deliveries
                .Include(d => d.Order)
                    .ThenInclude(o => o.CoproducerInfo)
                .Include(d => d.Order)
                    .ThenInclude(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                .ToListAsync();

            return _mapper.Map<IEnumerable<DeliveryDto>>(deliveries);
        }

        public async Task<DeliveryDto?> GetByIdAsync(int id, string? userId = null)
        {
            var delivery = await _context.Deliveries
                .Include(d => d.Order)
                    .ThenInclude(o => o.CoproducerInfo)
                .Include(d => d.Order)
                    .ThenInclude(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (delivery == null)
                return null;

            // Se userId é fornecido, verificar autorização
            if (!string.IsNullOrEmpty(userId))
            {
                // Verificar se o usuário é admin
                var user = await _userManager.FindByIdAsync(userId);
                var isAdmin = user != null && await _userManager.IsInRoleAsync(user, "Administrator");

                // Se não é admin, verificar se o usuário tem permissão para ver esta entrega
                if (!isAdmin)
                {
                    bool isAuthorized = await CanUserAccessDeliveryAsync(id, userId);
                    if (!isAuthorized)
                    {
                        throw new UnauthorizedAccessException("You don't have permission to view this delivery.");
                    }
                }
            }

            return _mapper.Map<DeliveryDto>(delivery);
        }

        public async Task<DeliveryDto> CreateAsync(CreateDeliveryDto dto)
        {
            try
            {
                // Verificar se a order existe
                var order = await _context.Orders
                    .FirstOrDefaultAsync(o => o.Id == dto.OrderId);

                if (order == null)
                    throw new KeyNotFoundException($"Order with ID {dto.OrderId} not found.");

                // Verificar se já existe uma entrega para esta order
                var existingDelivery = await _context.Deliveries
                    .FirstOrDefaultAsync(d => d.OrderId == dto.OrderId);

                if (existingDelivery != null)
                    throw new ArgumentException($"A delivery already exists for order ID {dto.OrderId}.");

                var delivery = new Delivery
                {
                    OrderId = dto.OrderId,
                    DeliveryDate = dto.DeliveryDate,
                    DeliveryLocation = dto.DeliveryLocation,
                    Status = dto.Status
                };

                _context.Deliveries.Add(delivery);
                await _context.SaveChangesAsync();

                // Recarregar a entrega com dados relacionados
                var fullDelivery = await _context.Deliveries
                    .Include(d => d.Order)
                        .ThenInclude(o => o.CoproducerInfo)
                    .Include(d => d.Order)
                        .ThenInclude(o => o.OrderItems)
                            .ThenInclude(oi => oi.Product)
                    .FirstOrDefaultAsync(d => d.Id == delivery.Id);

                return _mapper.Map<DeliveryDto>(fullDelivery);
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Error saving the delivery to the database: " + ex.InnerException?.Message, ex);
            }
        }

        public async Task<DeliveryDto?> UpdateAsync(int id, UpdateDeliveryDto dto)
        {
            var delivery = await _context.Deliveries
                .Include(d => d.Order)
                    .ThenInclude(o => o.CoproducerInfo)
                .Include(d => d.Order)
                    .ThenInclude(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (delivery == null)
                throw new KeyNotFoundException($"Delivery with ID {id} not found");

            // Atualizar propriedades da entrega
            delivery.DeliveryDate = dto.DeliveryDate;
            delivery.DeliveryLocation = dto.DeliveryLocation;
            delivery.Status = dto.Status;

            await _context.SaveChangesAsync();

            return _mapper.Map<DeliveryDto>(delivery);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var delivery = await _context.Deliveries
                .FirstOrDefaultAsync(d => d.Id == id);

            if (delivery == null)
                return false;

            _context.Deliveries.Remove(delivery);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<DeliveryDto>> GetDeliveriesByProducerAsync(int producerId)
        {
            var deliveries = await _context.Deliveries
                .Include(d => d.Order)
                    .ThenInclude(o => o.CoproducerInfo)
                .Include(d => d.Order)
                    .ThenInclude(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                .Where(d => d.Order.OrderItems.Any(oi => oi.ProducerId == producerId))
                .ToListAsync();

            return _mapper.Map<IEnumerable<DeliveryDto>>(deliveries);
        }

        public async Task<IEnumerable<DeliveryDto>> GetDeliveriesByCoProducerAsync(int coproducerId)
        {
            var deliveries = await _context.Deliveries
                .Include(d => d.Order)
                    .ThenInclude(o => o.CoproducerInfo)
                .Include(d => d.Order)
                    .ThenInclude(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                .Where(d => d.Order.CoproducerInfoId == coproducerId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<DeliveryDto>>(deliveries);
        }

        public async Task<IEnumerable<DeliveryDto>> GetDeliveriesByUserIdAsync(string userId)
        {
            // Verificar se o usuário é admin
            var user = await _userManager.FindByIdAsync(userId);
            var isAdmin = user != null && await _userManager.IsInRoleAsync(user, "Administrator");

            if (isAdmin)
            {
                return await GetAllAsync();
            }

            // Buscar informações do coproducer e producer para este usuário
            var coproducerInfo = await _context.CoproducersInfo
                .FirstOrDefaultAsync(c => c.UserId == userId);

            var producerInfo = await _context.ProducersInfo
                .FirstOrDefaultAsync(p => p.UserId == userId);

            var query = _context.Deliveries
                .Include(d => d.Order)
                    .ThenInclude(o => o.CoproducerInfo)
                .Include(d => d.Order)
                    .ThenInclude(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                .AsQueryable();

            // Filtrar entregas relacionadas ao usuário
            query = query.Where(d =>
                (coproducerInfo != null && d.Order.CoproducerInfoId == coproducerInfo.Id) ||
                (producerInfo != null && d.Order.OrderItems.Any(oi => oi.ProducerId == producerInfo.Id))
            );

            var deliveries = await query.ToListAsync();
            return _mapper.Map<IEnumerable<DeliveryDto>>(deliveries);
        }

        public async Task<bool> IsUserAuthorizedForCoproducerAsync(string userId, int coproducerId)
        {
            var coproducerInfo = await _context.CoproducersInfo
                .FirstOrDefaultAsync(c => c.UserId == userId);

            return coproducerInfo != null && coproducerInfo.Id == coproducerId;
        }

        public async Task<bool> IsUserAuthorizedForProducerAsync(string userId, int producerId)
        {
            var producerInfo = await _context.ProducersInfo
                .FirstOrDefaultAsync(p => p.UserId == userId);

            return producerInfo != null && producerInfo.Id == producerId;
        }

        public async Task<bool> CanUserAccessDeliveryAsync(int deliveryId, string userId)
        {
            var delivery = await _context.Deliveries
                .Include(d => d.Order)
                    .ThenInclude(o => o.OrderItems)
                .FirstOrDefaultAsync(d => d.Id == deliveryId);

            if (delivery == null)
                return false;

            // Verificar se o usuário é o coproducer da order
            var coproducerInfo = await _context.CoproducersInfo
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (coproducerInfo != null && delivery.Order.CoproducerInfoId == coproducerInfo.Id)
                return true;

            // Verificar se o usuário é um producer com itens na order
            var producerInfo = await _context.ProducersInfo
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (producerInfo != null)
            {
                var hasItems = delivery.Order.OrderItems
                    .Any(oi => oi.ProducerId == producerInfo.Id);
                return hasItems;
            }

            return false;
        }
    }
}