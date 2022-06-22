using MarketplaceBackend.Contracts.V1.Requests.Orders;
using MarketplaceBackend.Contracts.V1.Responses.Orders;

namespace MarketplaceBackend.Services
{
    public interface IOrderService
    {
        Task<List<OrderBriefResponse>> GetOrdersAsync();

        Task<OrderBriefResponse> CreateOrderAsync(CreateOrderRequest request);
    }
}
