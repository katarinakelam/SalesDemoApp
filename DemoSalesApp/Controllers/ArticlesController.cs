using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DemoSalesApp.DLL.Repositories.ArticlesRepository;
using DemoSalesApp.ViewModels.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoSalesApp.Controllers
{
    /// <summary>
    /// The sales controller.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    [Route("articles")]
    [Produces("application/json")]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticlesRepository articlesRepository;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticsController"/> class.
        /// </summary>
        /// <param name="articlesRepository">The articles repository.</param>
        /// <param name="mapper"></param>
        /// <exception cref="System.ArgumentNullException">salesRepository</exception>
        public ArticlesController(IArticlesRepository articlesRepository,
            IMapper mapper)
        {
            this.articlesRepository = articlesRepository ?? throw new ArgumentNullException(nameof(articlesRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Gets the articles.
        /// </summary>
        /// <returns>
        /// Returns a list of all articles in the database.
        /// </returns>
        /// <remarks>
        /// Sample request:
        ///     GET /articles
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetArticles()
        {
            try
            {
                var articles = this.articlesRepository.GetAllArticles();
                var mappedArticles = this.mapper.Map<List<ArticleViewModel>>(articles);

                return this.StatusCode(StatusCodes.Status200OK, mappedArticles);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
