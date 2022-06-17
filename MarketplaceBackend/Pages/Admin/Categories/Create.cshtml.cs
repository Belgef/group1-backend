using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MarketplaceBackend.Data;
using MarketplaceBackend.Models;
using MarketplaceBackend.Services;
using System.ComponentModel.DataAnnotations;

namespace MarketplaceBackend.Pages.Admin.Categories
{
    public class CreateModel : PageModel
    {
        private readonly MarketplaceBackend.Data.DataContext _context;
        private readonly IFileService _fileService;
        private readonly IConfiguration _configuration;

        public CreateModel(MarketplaceBackend.Data.DataContext context, IFileService fileService, IConfiguration configuration)
        {
            _context = context;
            _fileService = fileService;
            _configuration = configuration;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Category Category { get; set; }

        [Required]
        [BindProperty]
        public IFormFile FormFile { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Categories.Add(Category);
            await _context.SaveChangesAsync();

            await _fileService.UploadFileAsync(FormFile, $"category_{Category.Id}");

            Category.ImageURL = $"https://{_configuration.GetValue<string>("AWS:BucketName")}.s3.amazonaws.com/category_{Category.Id}";
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
