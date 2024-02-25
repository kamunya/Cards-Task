using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Cards.Core.Entities.Identity;
using Cards.Infrastracture.Data;
using Cards.Infrastracture.identity;
using Cards.Infrastracture.SeedData;
using System;
using System.Threading.Tasks;

namespace Cards
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerfactory = scope.ServiceProvider.GetService<ILoggerFactory>();
                try
                {
                    var context = services.GetRequiredService<CardsContext>();
                    await context.Database.MigrateAsync();
                    await CardsSeed.SeedAsync(context, loggerfactory);
                    var userManager = services.GetRequiredService<UserManager<AppUser>>();
                    var identityContext = services.GetRequiredService<IdentityContext>();
                    await identityContext.Database.MigrateAsync();
                    await AppIdentityDbContextSeed.SeedUserData(userManager);
                }
                catch (Exception ex)
                {
                    var loger = loggerfactory.CreateLogger<CardsContext>();
                    loger.LogError(ex, "Data migration failed.");
                }
            }
            host.Run();
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
