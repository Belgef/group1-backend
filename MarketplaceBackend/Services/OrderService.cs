using AutoMapper;
using MarketplaceBackend.Contracts.V1.Requests.Orders;
using MarketplaceBackend.Contracts.V1.Responses.Orders;
using MarketplaceBackend.Data;
using MarketplaceBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceBackend.Services
{
    public class OrderService : IOrderService
    {

        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IProductService _productService;
        public OrderService(IMapper mapper, DataContext dataContext, ICurrentUserService currentUserService, IProductService productService)
        {
            _mapper = mapper;
            _dataContext = dataContext;
            _currentUserService = currentUserService;
            _productService = productService;
        }

        public async Task<OrderBriefResponse> CreateOrderAsync(CreateOrderRequest request)
        {
            var userId = _currentUserService.UserId;

            var order = new Order
            {
                OnDate = DateTime.UtcNow,
                UserId = userId
            };

            foreach (var orderProductDto in request.OrderProducts)
            {
                var orderProduct = new OrderProduct
                {
                    Order = order,
                    ProductId = orderProductDto.ProductId,
                    UnitPrice = (await _productService.GetProductByIdAsync(orderProductDto.ProductId)).Price,
                    Quantity = orderProductDto.Quantity
                };
                order.OrderProducts.Add(orderProduct);
                _dataContext.OrderProducts.Add(orderProduct);
            }

            _dataContext.Orders.Add(order);

            await _dataContext.SaveChangesAsync();

            return _mapper.Map<OrderBriefResponse>(order);
        }

        public async Task<List<OrderBriefResponse>> GetOrdersAsync()
        {
            var orders = await _dataContext.Orders
                .Include(o => o.OrderProducts)
                .Where(o => o.UserId == _currentUserService.UserId).ToListAsync();

            return _mapper.Map<List<OrderBriefResponse>>(orders);
        }
    }
}
