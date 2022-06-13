using AutoMapper;
using MarketplaceBackend.Contracts.V1.Responses.Identity;
using MarketplaceBackend.Data;
using MarketplaceBackend.Models;
using MarketplaceBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService, IMapper mapper)
        {
            _identityService = identityService;
            _mapper = mapper;
        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <returns>Created JWT Token</returns>
        /// <response code="200">Returns user's data and generated JWT Token</response>
        /// <response code="400">Returns array of errors</response>
        [HttpPost("/login")]
        public async Task<ActionResult<AuthSuccessResponse>> Login([FromBody] UserLoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors, (_, error) => error.ErrorMessage)
                });
            }

            var authResponse = await _identityService.LoginAsync(request.Email, request.Password);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(_mapper.Map<AuthSuccessResponse>(authResponse));
        }

        /// <summary>
        /// Register user
        /// </summary>
        /// <response code="200">Returns user's data and generated JWT Token</response>
        /// <response code="400">Returns array of errors</response>
        [HttpPost("/register")]
        public async Task<ActionResult<AuthSuccessResponse>> Register([FromBody] UserRegistrationRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors, (_, error) => error.ErrorMessage)
                });
            }

            var authResponse = await _identityService.RegisterAsync(request);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(_mapper.Map<AuthSuccessResponse>(authResponse));
        }
    }
}
