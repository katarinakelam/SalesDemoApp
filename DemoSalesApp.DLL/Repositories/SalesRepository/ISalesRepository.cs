using System;
using System.Collections.Generic;
using System.Text;
using DemoSalesApp.Models;
using DemoSalesApp.ViewModels.DTOs;

namespace DemoSalesApp.DLL.Repositories.SalesRepository
{
    /// <summary>
    /// The sales repository.
    /// </summary>
    public interface ISalesRepository
    {
        /// <summary>
        /// Creates the sale event.
        /// </summary>
        /// <param name="saleEventDTO">The sale event dto.</param>
        /// <returns>
        /// Returns a sale event object from the database created by the action.
        /// </returns>
        SaleEvent CreateSaleEvent(SaleEventDTO saleEventDTO);

        /// <summary>
        /// Gets the sold articles per day.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>
        /// Returns number of sold articles for given date.
        /// </returns>
        int GetSoldArticlesPerDay(DateTime? date);

        /// <summary>
        /// Gets the total revenue per day.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>
        /// Returns amount of total revenue for given date.
        /// </returns>
        double GetTotalRevenuePerDay(DateTime? date);

        /// <summary>
        /// Gets the revenue grouped by articles.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>
        /// Returns calculated revenues grouped by articles.
        /// </returns>
        List<Tuple<string, double>> GetRevenueGroupedByArticles(DateTime? startDate, DateTime? endDate);
    }
}
