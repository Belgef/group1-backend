using MarketplaceBackend.Common.Pagination;
using MarketplaceBackend.Contracts.V1.Requests.Products;
using MarketplaceBackend.Contracts.V1.Responses.Products;
using MarketplaceBackend.Models;

namespace MarketplaceBackend.Services
{
    public interface IProductService
    {
        Task<PaginatedList<ProductBriefResponse>> GetProductsAsync(GetAllProductsQuery request);

        Task<int> CreateProductAsync(CreateProductRequest product);

        Task<Product> GetProductByIdAsync(int productId);

        Task<bool> UpdateProductAsync(UpdateProductRequest request);

        Task<bool> DeleteProductAsync(int productId);
    }
}
