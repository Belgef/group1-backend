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

        [Authorize(Roles = "Admin")]
        [HttpPost(ApiRoutes.Products.Create)]
        public async Task<ActionResult<int>> Create(CreateProductRequest request)
        {
            var id = await _productService.CreateProductAsync(request);

            return Created(ApiRoutes.Products.Create + "/" + id, id);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut(ApiRoutes.Products.Update)]
        public async Task<ActionResult> Update(int id, UpdateProductRequest request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            var updated = await _productService.UpdateProductAsync(request);
            if (updated)
                return NoContent();

            return NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete(ApiRoutes.Products.Delete)]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _productService.DeleteProductAsync(id);

            if (deleted)
                return NoContent();

            return NotFound();
        }
    }
}
