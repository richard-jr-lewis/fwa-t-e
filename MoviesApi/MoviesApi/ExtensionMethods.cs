using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoviesApi
{
    public static class ExtensionMethods
    {
        public static IWebHost Populate(this IWebHost webhost)
        {
            using (var scope = webhost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    SeedData.Initialize(services);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurrred seeding the DB.");
                }
            }

            return webhost;
        }

        /// <summary>
        /// http://www.extensionmethod.net/1735/csharp/ienumerable-t/whereif
        /// </summary>
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, bool> predicate)
        {
            return condition ? source.Where(predicate) : source;
        }
    }
}
