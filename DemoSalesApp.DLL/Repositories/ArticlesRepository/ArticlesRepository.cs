using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DemoSalesApp.Models;

namespace DemoSalesApp.DLL.Repositories.ArticlesRepository
{
    /// <summary>
    /// The articles repository.
    /// </summary>
    /// <seealso cref="DemoSalesApp.DLL.Repositories.ArticlesRepository.IArticlesRepository" />
    public class ArticlesRepository : IArticlesRepository
    {
        private readonly DataContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticlesRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <exception cref="System.ArgumentNullException">context</exception>
        public ArticlesRepository(DataContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Gets all articles.
        /// </summary>
        /// <returns>
        /// Returns a list of all articles.
        /// </returns>
        /// <exception cref="System.NullReferenceException">Articles</exception>
        public List<Article> GetAllArticles()
        {
            if (this.context.Articles == null)
                throw new NullReferenceException(nameof(this.context.Articles));

            return this.context.Articles.OrderBy(a => a.Name).ToList();
        }
    }
}
