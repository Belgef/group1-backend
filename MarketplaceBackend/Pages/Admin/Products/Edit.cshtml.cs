using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MarketplaceBackend.Data;
using MarketplaceBackend.Models;
using MarketplaceBackend.Services;
using System.Globalization;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MarketplaceBackend.Pages.Admin.Products
{
    public class EditModel : PageModel
    {
        private readonly MarketplaceBackend.Data.DataContext _context;
        private readonly IFileService _fileService;
        private readonly IConfiguration _configuration;

        public EditModel(MarketplaceBackend.Data.DataContext context, IFileService fileService, IConfiguration configuration)
        {
            _context = context;
            _fileService = fileService;
            _configuration = configuration;
        }

        [BindProperty]
        public Product Product { get; set; }

        [BindProperty]
        public IFormFile FormFile { get; set; }

        [BindProperty]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(14, 2)")]
        public string PriceString { get; set; }

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
           ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Product.Price = decimal.Parse(PriceString.Replace(",", "."), CultureInfo.InvariantCulture);

            _context.Attach(Product).State = EntityState.Modified;
            Product.ImageURL = $"https://{_configuration.GetValue<string>("AWS:BucketName")}.s3.amazonaws.com/product_{Product.Id}";
            if (FormFile is not null)
                await _fileService.UploadFileAsync(FormFile, $"product_{Product.Id}");

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
