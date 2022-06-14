using AutoMapper;
using AutoMapper.QueryableExtensions;
using MarketplaceBackend.Common.Mappings;
using MarketplaceBackend.Common.Pagination;
using MarketplaceBackend.Contracts.V1.Requests.Products;
using MarketplaceBackend.Contracts.V1.Responses.Products;
using MarketplaceBackend.Data;
using MarketplaceBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceBackend.Services
{
    public class ProductService : IProductService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public ProductService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }


        public async Task<PaginatedList<ProductBriefResponse>> GetProductsAsync(GetAllProductsQuery request)
        {
            var query = _dataContext.Products.AsNoTracking();
            var filtered = query;
            if (request.CategoryId != 0)
            {
                filtered = filtered.Where(x => x.CategoryId == request.CategoryId);
            }
            filtered = string.IsNullOrWhiteSpace(request.Search) ? filtered : filtered.Where(x => x.Name.Contains(request.Search));
            var ordered = filtered.OrderBy(x => x.Name);

            return await ordered
                .ProjectTo<ProductBriefResponse>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _dataContext.Products
                .Include(p => p.Category)
                .SingleOrDefaultAsync(x => x.Id == productId);
        }

        public async Task<bool> CreateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateProductAsync(Product productToUpdate)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            throw new NotImplementedException();
        }
        
    }
}
