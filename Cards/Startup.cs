using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Cards.Infrastracture.Data;
using Microsoft.EntityFrameworkCore;
using Cards.Helpers;
using Cards.ExceptionMiddleWare;
using Cards.Controllers.Extensions;
using StackExchange.Redis;
using Cards.Infrastracture.identity;
using Cards.Extensions;

namespace Cards
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CardsContext>(options => 
            options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<IdentityContext>(options =>
            options.UseSqlite(Configuration.GetConnectionString("DefaultIdentityConnection")));
            services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                var configuration = ConfigurationOptions.
                Parse(Configuration.GetConnectionString("Radis"), true);
                return ConnectionMultiplexer.Connect(configuration);
            });
           
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddControllers();
            services.AddApplicationServices();
            services.AddSwaggerDocumentation();
            services.AddIdentityService(Configuration);
            services.AddCors(options =>
            {
                 options.AddPolicy("CorsPolicy", policy =>
                 {
                     policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("*");
                 });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddle>();
            app.UseStatusCodePagesWithReExecute("/error/{0}");
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseSwaggerGen();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
