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
        public OrderService(IMapper mapper, DataContext dataContext, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _dataContext = dataContext;
            _currentUserService = currentUserService;
        }

        public async Task<OrderBriefResponse> CreateOrderAsync(CreateOrderRequest request)
        {
            var userId = _currentUserService.UserId;

            var order = new Order
            {
                OnDate = request.OnDate,
                UserId = userId
            };

            foreach (var orderProductDto in request.OrderProducts)
            {
                var orderProduct = new OrderProduct
                {
                    Order = order,
                    ProductId = orderProductDto.ProductId,
                    UnitPrice = orderProductDto.UnitPrice,
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
            var orders = await _dataContext.Orders.Where(o => o.UserId == _currentUserService.UserId).ToListAsync();

            return _mapper.Map<List<OrderBriefResponse>>(orders);
        }
    }
}
