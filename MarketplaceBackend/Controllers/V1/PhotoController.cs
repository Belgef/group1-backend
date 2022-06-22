﻿using MarketplaceBackend.Contracts.V1;
using MarketplaceBackend.Data;
using MarketplaceBackend.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceBackend.Controllers.V1
{
    [ApiController]
    public class PhotoController : ControllerBase
    {
        
        private readonly DataContext _dataContext;

        public PhotoController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <summary>
        /// Get carousel images
        /// </summary>
        /// <returns>List of carousel images</returns>
        /// <response code="200">Returns list of images for carousel</response>
        [HttpGet(ApiRoutes.Photos.GetAll)]
        public async Task<ActionResult<List<Photo>>> Get()
        {
            var response = await _dataContext.Photos.AsNoTracking().ToListAsync();
            return Ok(response);
        }

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        //[HttpPost(ApiRoutes.Carousel.Create)]
        //public async Task<ActionResult<int>> Post(string url)
        //{
        //    var carouselImage = new CarouselImage { ImageURL = url };

        //    await _dataContext.CarouselImages.AddAsync(carouselImage);
        //    await _dataContext.SaveChangesAsync();
        //    return carouselImage.Id;
        //}

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        //[HttpDelete(ApiRoutes.Carousel.Delete)]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var carouselImage = await _dataContext.CarouselImages.FirstOrDefaultAsync(x => x.Id == id);

        //    if (carouselImage is null)
        //        return NotFound();

        //    _dataContext.CarouselImages.Remove(carouselImage);
        //    await _dataContext.SaveChangesAsync();

        //    return NoContent();
        //}
    }
}
