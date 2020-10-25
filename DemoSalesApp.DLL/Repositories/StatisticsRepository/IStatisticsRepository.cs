using System;
using System.Collections.Generic;
using System.Text;

namespace DemoSalesApp.DLL.Repositories.StatisticsRepository
{
    /// <summary>
    /// The statistics repository.
    /// </summary>
    public interface IStatisticsRepository
    {
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
