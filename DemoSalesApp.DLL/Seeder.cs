using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using DemoSalesApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DemoSalesApp.DLL
{
    /// <summary>
    /// The database seeder.
    /// </summary>
    public static class Seeder
    {
        /// <summary>
        /// Initializes the database seeder with the given service provider.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new DataContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<DataContext>>());

            if (!context.Articles.Any())
            {
                context.Articles.AddRange(
                    new Article { ArticleNumber = "d1f2-b26e-407d-977c-cb5f", Name = "Banana" },
                    new Article { ArticleNumber = "64fe-7a3b-43ef-95b1-4126", Name = "Apple" },
                    new Article { ArticleNumber = "bdb3-bb72-460c-9151-cde6", Name = "Pear" },
                    new Article { ArticleNumber = "4c9c-7d26-4d3f-88f2-0f64", Name = "Mango" }
                   );
            }

            context.SaveChanges();
        }
    }
}
