﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DemoSalesApp.DLL.Repositories.SalesRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoSalesApp.Controllers
{
    /// <summary>
    /// The statistics controller.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    [Route("statistics")]
    [Produces("application/json")]
    public class StatisticsController : ControllerBase
    {
        private readonly ISalesRepository salesRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticsController"/> class.
        /// </summary>
        /// <param name="salesRepository">The sales repository.</param>
        /// <exception cref="System.ArgumentNullException">salesRepository</exception>
        public StatisticsController(ISalesRepository salesRepository)
        {
            this.salesRepository = salesRepository ?? throw new ArgumentNullException(nameof(salesRepository));
        }


#pragma warning disable CS1570 // XML comment has badly formed XML
        /// <summary>
        /// Gets the total revenue by articles.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>
        /// Returns a list of article identifiers and their matching revenue in matching date period, if date period is not sent, then for all time
        /// </returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /statistics/revenue-by-articles?startDate=2020-01-25&endDate=2020-10-15
        /// </remarks>
        [HttpGet]
#pragma warning restore CS1570 // XML comment has badly formed XML
        [Route("revenue-by-articles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetTotalRevenueByArticles(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var revenueByArticles = this.salesRepository.GetRevenueGroupedByArticles(startDate, endDate);

                return this.StatusCode(StatusCodes.Status200OK, revenueByArticles);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
