using MarketplaceBackend.Models;

namespace MarketplaceBackend.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategoriesAsync();
    }
}
