using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace API.Extensions;

public static class ApplicationExtensions
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
	{
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen(c =>
		{
			c.SwaggerDoc("v1", new OpenApiInfo { Title = "SkiNet API", Version = "v1" });
		});
		services.AddDbContext<StoreContext>(options =>
		{
			options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
		});
		services.AddScoped<IProductRepository, ProductRepository>();
		services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
		services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
		services.Configure<ApiBehaviorOptions>(options =>
		{
			options.InvalidModelStateResponseFactory = actionContext =>
			{
				var errors = actionContext.ModelState
					.Where(x => x.Value.Errors.Count > 0)
					.SelectMany(x => x.Value.Errors)
					.Select(x => x.ErrorMessage).ToArray();

				var errorResponse = new ApiValidationErrorResponse
				{
					Errors = errors
				};

				return new BadRequestObjectResult(errorResponse);
			};
		});

		services.AddCors(options =>
		{
			options.AddPolicy("CorsPolicy", policy =>
			{
				policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
			});
		});

		return services;
	}
}
