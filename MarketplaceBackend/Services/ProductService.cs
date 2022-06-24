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
            filtered = string.IsNullOrWhiteSpace(request.Search) ? filtered : filtered.Where(x => x.Name.ToLower().Contains(request.Search.ToLower()));
            var ordered = filtered.OrderBy(x => x.Name);

            var result = await ordered.ProjectTo<ProductBriefResponse>(_mapper.ConfigurationProvider).ToListAsync();
            result.ForEach(product => { product.SoldCount = _dataContext.OrderProducts.Where(x => x.ProductId == product.Id).Sum(x => x.Quantity); });

            return result.ToPaginatedList(request.PageNumber, request.PageSize);
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _dataContext.Products
                .Include(p => p.Category)
                .SingleOrDefaultAsync(x => x.Id == productId);
        }

        public async Task<int> CreateProductAsync(CreateProductRequest request)
        {
            var product = new Product
            {
                Name = request.Name,
                Price = request.Price,
                Description = request.Description,
                CategoryId = request.CategoryId
            };

            await _dataContext.Products.AddAsync(product);

            await _dataContext.SaveChangesAsync();

            return product.Id;

        }

        public async Task<bool> UpdateProductAsync(UpdateProductRequest request)
        {
            var product = await GetProductByIdAsync(request.Id);
            product.Name = request.Name;
            product.Price = request.Price;
            product.Description = request.Description;
            //product.ExtraInformation = request.ExtraInformation;
            product.CategoryId = request.CategoryId;

            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            var product = await GetProductByIdAsync(productId);
            if (product == null)
                return false;

            _dataContext.Products.Remove(product);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }
        
    }
}
