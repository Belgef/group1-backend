using MarketplaceBackend.Data;
using MarketplaceBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceBackend.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _dataContext;

        public CategoryService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            var query = _dataContext.Categories.AsNoTracking();

            var ordered = await query.OrderBy(x => x.Name).ToListAsync();

            return ordered;
        }
    }
}
