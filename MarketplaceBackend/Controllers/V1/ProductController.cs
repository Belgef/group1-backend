using AutoMapper;
using MarketplaceBackend.Common.Pagination;
using MarketplaceBackend.Contracts.V1;
using MarketplaceBackend.Contracts.V1.Requests.Products;
using MarketplaceBackend.Contracts.V1.Responses.Products;
using MarketplaceBackend.Services;
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

        //[HttpGet(ApiRoutes.Products.Get)]
        //public async Task<ActionResult<ProductResponse>> Get(int id)
        //{
        //    return Ok();
        //}

        //[HttpPost(ApiRoutes.Products.Create)]
        //public async Task<ActionResult<int>> Create(CreateProductRequest command)
        //{
        //    return Created();
        //}

        //[HttpPut(ApiRoutes.Products.Update)]
        //public async Task<ActionResult> Update(int id, UpdateProductRequest command)
        //{
        //    if (id != command.Id)
        //    {
        //        return BadRequest();
        //    }

        //    await Mediator.Send(command);

        //    return NoContent();
        //}

        //[HttpDelete(ApiRoutes.Products.Delete)]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    await Mediator.Send(new DeleteProductRequest { Id = id });

        //    return NoContent();
        //}
    }
}
