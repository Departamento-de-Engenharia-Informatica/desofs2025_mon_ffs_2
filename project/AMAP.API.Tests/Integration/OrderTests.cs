using AMAPP.API.Data;
using AMAPP.API.DTOs.Order;
using AMAPP.API.Models;
using AMAPP.API.Services.Implementations;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;

using static AMAPP.API.Constants;

public class OrderServiceTests
{
    private readonly ApplicationDbContext _context;
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly Mock<UserManager<User>> _userManagerMock;
    private readonly OrderService _orderService;

    public OrderServiceTests()
    {
        // Setup InMemory DB
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);

        // Setup UserManager mock
        var userStoreMock = new Mock<IUserStore<User>>();
        _userManagerMock = new Mock<UserManager<User>>(userStoreMock.Object, null, null, null, null, null, null, null, null);

        // Create service
        _orderService = new OrderService(_context, _mapperMock.Object, _userManagerMock.Object);
    }

    [Fact]
    public async Task GetOrdersAsync_ShouldFilterOrdersByProducerId()
    {
        // Arrange
        var producer = new ProducerInfo { Id = 1, UserId = "producer-user" };
        var product = new Product
        {
            Id = 1,
            ProducerInfoId = producer.Id,
            ReferencePrice = 2.5,
            DeliveryUnit = "kg",
            Description = "Test product",
            Name = "Tomatoes"
        };

        var coproducer = new CoproducerInfo { Id = 1, UserId = "coproducer-user" };

        _context.ProducersInfo.Add(producer);
        _context.Products.Add(product);
        _context.CoproducersInfo.Add(coproducer);
        await _context.SaveChangesAsync();

        var order = new Order
        {
            Id = 1,
            CoproducerInfoId = coproducer.Id,
            OrderDate = DateTime.UtcNow,
            Status = OrderStatus.Pending,
            DeliveryRequirements = "Normal",
            OrderItems = new List<OrderItem>
    {
        new OrderItem
        {
            ProductId = product.Id,
            Quantity = 3,
            Price = product.ReferencePrice
        }
    }
        };
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        var user = new User
        {
            Id = "user-1",
            UserName = "testuser",
            FirstName = "John",
            LastName = "Doe"
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        _userManagerMock.Setup(um => um.FindByIdAsync("producer-user")).ReturnsAsync(user);
        _userManagerMock.Setup(um => um.IsInRoleAsync(user, "Administrator")).ReturnsAsync(false);

        var filter = new OrderFilterDTO { ProducerId = producer.Id };

        _mapperMock.Setup(m => m.Map<IEnumerable<OrderDTO>>(It.IsAny<IEnumerable<Order>>()))
            .Returns(new List<OrderDTO> { new OrderDTO { Id = 1 } });

        // Act
        var result = await _orderService.GetOrdersAsync(filter, "producer-user");

        // Assert
        Assert.Single(result);
        Assert.Equal(1, result.First().Id);
    }

  /*  [Fact]
    public async Task CreateOrderAsync_ShouldCreateNewOrder_WhenProductExists()
    {
        // Arrange
        var user = new User
        {
            Id = "user-1",
            UserName = "testuser",
            FirstName = "John",
            LastName = "Doe"
        };
        _context.Users.Add(user);

        // Create producer first
        var producer = new ProducerInfo { Id = 1, UserId = "prod-user" };
        _context.ProducersInfo.Add(producer);

        // Create coproducer info for the user creating the order
        var coproducer = new CoproducerInfo { Id = 1, UserId = "user-1" };
        _context.CoproducersInfo.Add(coproducer);

        // Save these entities first
        await _context.SaveChangesAsync();

        // Now create the product with the saved producer
        var product = new Product
        {
            Id = 1,
            ProducerInfoId = producer.Id,
            ReferencePrice = 1.5,
            DeliveryUnit = "kg",
            Description = "Test product",
            Name = "Tomatoes"
        };
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        // Debug: Verify the product exists in the database
        var productInDb = await _context.Products.FindAsync(1);
        Assert.NotNull(productInDb); // This should pass if the product was saved

        // Debug: Check all products in the database
        var allProducts = await _context.Products.ToListAsync();
        Assert.Single(allProducts); // Should have exactly one product

        _userManagerMock.Setup(u => u.FindByIdAsync("user-1")).ReturnsAsync(user);

        var createOrderDTO = new CreateOrderDTO
        {
            DeliveryRequirements = "Fast",
            OrderItems = new List<CreateOrderItemDTO>
            {
                new CreateOrderItemDTO { ProductId = 1, Quantity = 2 }
            }
        };

        _mapperMock.Setup(m => m.Map<OrderDTO>(It.IsAny<Order>())).Returns(new OrderDTO { Id = 1 });

        // Act
        var result = await _orderService.CreateOrderAsync(createOrderDTO, "user-1");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);

        // Verify the order was actually created in the database
        var createdOrder = await _context.Orders.FirstOrDefaultAsync();
        Assert.NotNull(createdOrder);
        Assert.Equal("Fast", createdOrder.DeliveryRequirements);
    } */

    [Fact]
    public async Task UpdateOrderAsync_ShouldUpdateDeliveryRequirements_WhenUserIsAdmin()
    {
        // Arrange
        var user = new User
        {
            Id = "user-1",
            UserName = "testuser",
            FirstName = "John",
            LastName = "Doe"
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        _userManagerMock.Setup(u => u.FindByIdAsync("admin-user")).ReturnsAsync(user);
        _userManagerMock.Setup(u => u.IsInRoleAsync(user, "Administrator")).ReturnsAsync(true);

        var coproducer = new CoproducerInfo { Id = 10, UserId = "admin-user" };
        _context.CoproducersInfo.Add(coproducer);
        await _context.SaveChangesAsync();

        var order = new Order
        {
            Id = 50,
            CoproducerInfoId = coproducer.Id,
            DeliveryRequirements = "Initial",
            Status = OrderStatus.Pending,
            OrderDate = DateTime.UtcNow
        };
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        var updateDto = new UpdateOrderDTO
        {
            DeliveryRequirements = "Updated delivery",
            Status = OrderStatus.Confirmed
        };

        _mapperMock.Setup(m => m.Map<OrderDTO>(It.IsAny<Order>()))
            .Returns(new OrderDTO { Id = 50 });

        // Act
        var result = await _orderService.UpdateOrderAsync(50, updateDto, "admin-user");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(50, result.Id);

        // Verify the order was actually updated in the database
        var updatedOrder = await _context.Orders.FindAsync(50);
        Assert.NotNull(updatedOrder);
        Assert.Equal("Updated delivery", updatedOrder.DeliveryRequirements);
        Assert.Equal(OrderStatus.Confirmed, updatedOrder.Status);
    }
}