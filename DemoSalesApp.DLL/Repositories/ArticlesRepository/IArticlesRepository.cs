using System;
using System.Collections.Generic;
using System.Text;
using DemoSalesApp.Models;

namespace DemoSalesApp.DLL.Repositories.ArticlesRepository
{
    /// <summary>
    /// The articles repository.
    /// </summary>
    public interface IArticlesRepository
    {
        /// <summary>
        /// Gets all articles.
        /// </summary>
        /// <returns>
        /// Returns a list of all articles.
        /// </returns>
        List<Article> GetAllArticles();
    }
}
