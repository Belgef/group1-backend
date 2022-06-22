using AutoMapper;
using MarketplaceBackend.Contracts.V1;
using MarketplaceBackend.Contracts.V1.Requests.Orders;
using MarketplaceBackend.Contracts.V1.Responses.Orders;
using MarketplaceBackend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceBackend.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Get user's orders
        /// </summary>
        /// <returns>List of user's orders</returns>
        /// <response code="200">Returns list of user's orders</response>
        [HttpGet(ApiRoutes.Orders.GetAll)]
        public async Task<ActionResult<List<OrderBriefResponse>>> Get()
        {
            var response = await _orderService.GetOrdersAsync();
            return Ok(response);
        }

        [HttpPost(ApiRoutes.Orders.Create)]
        public async Task<ActionResult<OrderBriefResponse>> Create([FromBody] CreateOrderRequest request)
        {
            var response = await _orderService.CreateOrderAsync(request);
            return Ok(response);
        }
    }
}
