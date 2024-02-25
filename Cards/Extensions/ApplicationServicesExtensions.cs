using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Cards.Application.CustomServices;
using Cards.Core.Interfaces;
using Cards.Errors;
using Cards.Infrastracture.Data;
using Cards.Infrastracture.Repositories;
using Cards.Infrastracture.SeedData;
using System.Linq;

namespace Cards.Controllers.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<CardsContext, CardsContext>();
            services.AddScoped<CardsSeed, CardsSeed>();
            services.AddScoped<ICardRepository, CardRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.Configure<ApiBehaviorOptions>(options =>
              options.InvalidModelStateResponseFactory = ActionContext =>
              {
                  var error = ActionContext.ModelState
                              .Where(e => e.Value.Errors.Count > 0)
                              .SelectMany(e => e.Value.Errors)
                              .Select(e => e.ErrorMessage).ToArray();
                  var errorresponce = new APIValidationErrorResponce
                  {
                      Errors = error
                  };
                  return new BadRequestObjectResult(error);
              }
            );
            return services;
        }
    }
}
