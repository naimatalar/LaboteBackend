using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core
{
    public static class AutoDbUpdate
    {


        public static IHost MigrateDbContext<TContext>(this IHost host) where TContext : DbContext
        {
            //TContext.Database.EnsureCreated();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();
                //context.Database.EnsureCreated();
                try
                {
                    logger.LogInformation($"Migrating database associated with context {typeof(TContext).Name}");
                    var retry = Policy.Handle<Exception>().WaitAndRetry(new[]
                    {
                TimeSpan.FromSeconds(5),
                TimeSpan.FromSeconds(10),
                TimeSpan.FromSeconds(15),
            });

                    retry.Execute(() =>
                    {
                        try
                        {
                            context.Database.Migrate();
                        }
                        catch
                        {
                        }

                    });
                    logger.LogInformation($"Migrated database associated with context {typeof(TContext).Name}");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"An error occurred while migrating the database used on context {typeof(TContext).Name}");
                }
            }

            return host;
        }
    }
}
