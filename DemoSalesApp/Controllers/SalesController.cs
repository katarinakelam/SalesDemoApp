using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DemoSalesApp.DLL.Repositories.SalesRepository;
using DemoSalesApp.ViewModels.DTOs;
using DemoSalesApp.ViewModels.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DemoSalesApp.Controllers
{
    /// <summary>
    /// The sales controller.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    [Route("sales")]
    [Produces("application/json")]
    public class SalesController : ControllerBase
    {
        private readonly ISalesRepository salesRepository;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="SalesController"/> class.
        /// </summary>
        /// <param name="salesRepository">The sales repository.</param>
        /// <param name="mapper"></param>
        /// <exception cref="System.ArgumentNullException">salesRepository</exception>
        public SalesController(ISalesRepository salesRepository,
            IMapper mapper)
        {
            this.salesRepository = salesRepository ?? throw new ArgumentNullException(nameof(salesRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Creates a sale event.
        /// </summary>
        /// <param name="saleEventDTO">The sale event dto.</param>
        /// <returns>
        /// Returns a created sales event object.
        /// </returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /sales
        ///     {
        ///        "articleSoldNumber": "ST12-sfsd5465-1dsfs5",
        ///        "articleSoldPrice": "12.99"
        ///     }
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateASaleEvent(SaleEventDTO saleEventDTO)
        {
            if (saleEventDTO == null)
                return this.BadRequest("The data sent has not reached the server. Please check the data and try again.");

            try
            {
                var newSaleEvent = this.salesRepository.CreateSaleEvent(saleEventDTO);

                var mappedEvent = this.mapper.Map<SaleEventViewModel>(newSaleEvent);

                return this.StatusCode(StatusCodes.Status201Created, mappedEvent);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gets the number of articles sold per day.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>
        /// Returns the number of articles sold per day specified, or for today if no date is specified.
        /// </returns>
        /// <remarks>
        /// Sample request:
        ///     GET /sales/articles-sold-per-day?date=2020-10-25
        ///     GET /sales/articles-sold-per-day
        /// </remarks>
        [HttpGet]
        [Route("articles-sold-per-day")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetNumberOfArticlesSoldPerDay(DateTime? date)
        {
            try
            {
                var articlesSoldPerDay = this.salesRepository.GetSoldArticlesPerDay(date);

                return this.StatusCode(StatusCodes.Status200OK, articlesSoldPerDay);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gets the total revenue per day.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>
        /// Returns the total revenue per day specified, or for today if no date is specified.
        /// </returns>
        /// <remarks>
        /// Sample request:
        ///     GET /sales/revenue-per-day?date=2020-10-25
        ///     GET /sales/revenue-per-day
        /// </remarks>
        [HttpGet]
        [Route("revenue-per-day")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetTotalRevenuePerDay(DateTime? date)
        {
            try
            {
                var revenuePerDay = this.salesRepository.GetTotalRevenuePerDay(date);

                return this.StatusCode(StatusCodes.Status200OK, revenuePerDay);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
