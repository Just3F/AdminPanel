using System.Linq;
using AdminPanel.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AdminPanel
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).MigrateDatabase().Run();
        }

        private static IWebHost MigrateDatabase(this IWebHost webHost)
        {
            var serviceScopeFactory = (IServiceScopeFactory)webHost.Services.GetService(typeof(IServiceScopeFactory));

            using (var scope = serviceScopeFactory.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<ApplicationContext>();

                if (!dbContext.vlGeneralSettings.Any())
                    dbContext.vlGeneralSettings.Add(new vlGeneralSettings());

                if (!dbContext.tblUser.Any())
                    dbContext.tblUser.Add(new tblUser
                    {
                        Email = "admin@admin.com",
                        Password = "admin"
                    });

                dbContext.Database.Migrate();
                dbContext.SaveChanges();
            }

            return webHost;
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
