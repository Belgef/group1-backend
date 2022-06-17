using AutoMapper;
using MarketplaceBackend.Common.Pagination;
using MarketplaceBackend.Contracts.V1;
using MarketplaceBackend.Contracts.V1.Requests.Products;
using MarketplaceBackend.Contracts.V1.Responses.Products;
using MarketplaceBackend.Models;
using MarketplaceBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceBackend.Controllers.V1
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly IIdentityService _identityService;

        public ProductController(IIdentityService identityService, IMapper mapper, IProductService productService)
        {
            _identityService = identityService;
            _mapper = mapper;
            _productService = productService;
        }

        /// <summary>
        /// Get filtered products
        /// </summary>
        /// <returns>List of products</returns>
        /// <response code="200">Returns list of products and pagination info</response>
        [HttpGet(ApiRoutes.Products.GetAll)]
        public async Task<ActionResult<PaginatedList<ProductBriefResponse>>> Get([FromQuery] GetAllProductsQuery request)
        {
            var response = await _productService.GetProductsAsync(request);
            return Ok(response);
        }

        [HttpGet(ApiRoutes.Products.Get)]
        public async Task<ActionResult<ProductResponse>> Get(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
                return NotFound();

            return Ok(_mapper.Map<ProductResponse>(product));
        }
    }
}
