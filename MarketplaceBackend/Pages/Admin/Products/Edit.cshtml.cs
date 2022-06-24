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
        private readonly DataContext _context;
        private readonly IFileService _fileService;
        private readonly IConfiguration _configuration;

        public EditModel(DataContext context, IFileService fileService, IConfiguration configuration)
        {
            _context = context;
            _fileService = fileService;
            _configuration = configuration;
        }

        [BindProperty]
        public Product Product { get; set; }

        [BindProperty]
        public IFormFile ProductImage { get; set; }

        [BindProperty]
        public IFormFile DetailsPicturePrimary { get; set; }

        [BindProperty]
        public IFormFile DetailsPictureSecondary1 { get; set; }

        [BindProperty]
        public IFormFile DetailsPictureSecondary2 { get; set; }

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
            PriceString = Product.Price.ToString();
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
            if (ProductImage is not null)
                await _fileService.UploadFileAsync(ProductImage, $"product_{Product.Id}");

            Product.DetailsPictureURLPrimary = Product.ImageURL + "_primary";
            if (DetailsPicturePrimary is not null)
            {
                await _fileService.UploadFileAsync(DetailsPicturePrimary, $"product_{Product.Id}_primary");
            }

            Product.DetailsPictureURLSecondary = new[] {
                Product.ImageURL + "_secondary1",
                Product.ImageURL + "_secondary2"
            };
            if (DetailsPictureSecondary1 is not null && DetailsPictureSecondary2 is not null)
            {
                
                await _fileService.UploadFileAsync(DetailsPictureSecondary1, $"product_{Product.Id}_secondary1");
                await _fileService.UploadFileAsync(DetailsPictureSecondary2, $"product_{Product.Id}_secondary2");
            }

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
