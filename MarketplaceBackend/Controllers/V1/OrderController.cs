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
        private readonly IProductService _productService;

        public OrderController(IOrderService orderService, IProductService productService)
        {
            _orderService = orderService;
            _productService = productService;
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

        /// <summary>
        /// Create new order
        /// </summary>
        /// <returns>Created order info</returns>
        /// <response code="200">Returns created order info</response>
        [HttpPost(ApiRoutes.Orders.Create)]
        public async Task<ActionResult<OrderBriefResponse>> Create([FromBody] CreateOrderRequest request)
        {
            foreach (var orderProduct in request.OrderProducts)
            {
                if (await _productService.GetProductByIdAsync(orderProduct.ProductId) is null)
                {
                    return BadRequest();
                }
            }

            var response = await _orderService.CreateOrderAsync(request);
            return Ok(response);
        }
    }
}
