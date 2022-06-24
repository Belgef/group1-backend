using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MarketplaceBackend.Data;
using MarketplaceBackend.Models;
using Microsoft.AspNetCore.Authorization;
using MarketplaceBackend.Services;

namespace MarketplaceBackend.Pages.Admin.Products
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly MarketplaceBackend.Data.DataContext _context;
        private readonly IFileService _fileService;

        public DeleteModel(MarketplaceBackend.Data.DataContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        [BindProperty]
        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Products
                .Include(p => p.Category).FirstOrDefaultAsync(m => m.Id == id);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Products.FindAsync(id);

            if (Product != null)
            {
                await _fileService.DeleteFileAsync($"product_{Product.Id}");
                await _fileService.DeleteFileAsync($"product_{Product.Id}_primary");
                await _fileService.DeleteFileAsync($"product_{Product.Id}_secondary1");
                await _fileService.DeleteFileAsync($"product_{Product.Id}_secondary2");
                _context.Products.Remove(Product);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
