using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoSalesApp.DLL.Repositories.StatisticsRepository
{
    /// <summary>
    /// The statistics repository.
    /// </summary>
    /// <seealso cref="DemoSalesApp.DLL.Repositories.StatisticsRepository.IStatisticsRepository" />
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly DataContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticsRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <exception cref="System.ArgumentNullException">context</exception>
        public StatisticsRepository(DataContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Gets the revenue grouped by articles.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>
        /// Returns calculated revenues grouped by articles.
        /// </returns>
        public List<Tuple<string, double>> GetRevenueGroupedByArticles(DateTime? startDate, DateTime? endDate)
        {
            if (this.context.SaleEvents == null)
                throw new NullReferenceException(nameof(this.context.SaleEvents));

            var sales = this.context.SaleEvents.AsQueryable();
            if (startDate.HasValue && endDate.HasValue)
            {
                sales = sales.Where(s => s.TimeStamp > startDate.Value && s.TimeStamp < endDate.Value);
            }
            else if (startDate.HasValue && !endDate.HasValue)
            {
                sales = sales.Where(s => s.TimeStamp > startDate.Value);
            }
            else if (!startDate.HasValue && endDate.HasValue)
            {
                sales = sales.Where(s => s.TimeStamp < endDate.Value);
            }

            var a = sales
                .GroupBy(a => a.ArticleSoldNumber)
                .Select(g => new
                {
                    Id = g.Key,
                    Price = g.Sum(g => g.ArticleSoldPrice)
                })
                .OrderByDescending(a => a.Price)
                .ToList();

            var tuples = new List<Tuple<string, double>>();
            a.ForEach(a => tuples.Add(new Tuple<string, double>(a.Id, a.Price)));
            return tuples;
        }
    }
}
