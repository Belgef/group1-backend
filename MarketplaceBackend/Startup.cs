using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using MarketplaceBackend.Data;
using System.Text;
using Microsoft.OpenApi.Models;
using System.Reflection;
using MarketplaceBackend.Helpers;
using MarketplaceBackend.Services;
using MarketplaceBackend.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MarketplaceBackend;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services)
    {
        var authSettings = new AuthSettings();
        Configuration.Bind(nameof(AuthSettings), authSettings);
        services.AddSingleton(authSettings);

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddScoped<IIdentityService, IdentityService>();

        services.AddScoped<IFileService, S3FileService>();

        services.AddSingleton<ICurrentUserService, CurrentUserService>();

        services.AddHttpContextAccessor();

        services.AddScoped<IProductService, ProductService>();

        services.AddScoped<ICategoryService, CategoryService>();

        services.AddScoped<IOrderService, OrderService>();


        // Add services to the container.
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            // TODO: Remove for using HTTPS
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = authSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = authSettings.Audience,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authSettings.Key)),
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero
            };
        });

        services.AddControllers();

        services.AddRazorPages(options =>
        {
            options.Conventions.AuthorizeFolder("/Admin");
        });

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Marketplace API",
                Version = "v1"
            });

            // add JWT Authentication
            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "JWT Authentication",
                Description = "Enter JWT Bearer token **_only_**",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer", // must be lower case
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
            c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, Array.Empty<string>()}
                });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        services.AddDbContext<DataContext>(options =>
        {
            options.UseNpgsql(Configuration.GetConnectionString("DeployConnection"), providerOptions => providerOptions.EnableRetryOnFailure());
            //options.UseNpgsql(Configuration.GetConnectionString("LocalConnection"));

        });

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseCookiePolicy(new CookiePolicyOptions()
        {
            MinimumSameSitePolicy = SameSiteMode.Strict
        });

        app.UseCors();

        app.UseAuthentication();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(x =>
        {
            x.MapControllers();
            x.MapRazorPages();
        });

    }
}