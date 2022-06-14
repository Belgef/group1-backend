using MarketplaceBackend.Common.Pagination;
using MarketplaceBackend.Contracts.V1.Requests.Products;
using MarketplaceBackend.Contracts.V1.Responses.Products;
using MarketplaceBackend.Models;

namespace MarketplaceBackend.Services
{
    public interface IProductService
    {
        Task<PaginatedList<ProductBriefResponse>> GetProductsAsync(GetAllProductsQuery request);

        Task<bool> CreateProductAsync(Product Product);

        Task<Product> GetProductByIdAsync(Guid ProductId);

        Task<bool> UpdateProductAsync(Product ProductToUpdate);

        Task<bool> DeleteProductAsync(Guid ProductId);
    }
}
