using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DemoSalesApp.Models;
using DemoSalesApp.ViewModels.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DemoSalesApp.DLL.Repositories.SalesRepository
{
    /// <summary>
    /// The sales repository.
    /// </summary>
    /// <seealso cref="DemoSalesApp.DLL.Repositories.SalesRepository.ISalesRepository" />
    public class SalesRepository : ISalesRepository
    {
        private readonly DataContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="SalesRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <exception cref="System.ArgumentNullException">context</exception>
        public SalesRepository(DataContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Creates the sale event.
        /// </summary>
        /// <param name="saleEventDTO">The sale event dto.</param>
        /// <returns>
        /// Returns a sale event object from the database created by the action.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">saleEventDTO</exception>
        /// <exception cref="System.ArgumentException">ArticleSoldNumber</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">ArticleSoldPrice</exception>
        /// <exception cref="System.NullReferenceException">
        /// SaleEvents
        /// or
        /// Articles
        /// or
        /// articleInDatabase
        /// </exception>
        public SaleEvent CreateSaleEvent(SaleEventDTO saleEventDTO)
        {
            if (saleEventDTO == null)
                throw new ArgumentNullException(nameof(saleEventDTO));

            if (string.IsNullOrEmpty(saleEventDTO.ArticleSoldNumber))
                throw new ArgumentException(nameof(saleEventDTO.ArticleSoldNumber));

            if (saleEventDTO.ArticleSoldPrice <= 0)
                throw new ArgumentOutOfRangeException(nameof(saleEventDTO.ArticleSoldPrice));

            this.ValidateDataPresenceInTheDatabase();

            var articleInDatabase = this.context.Articles.FirstOrDefault(a => a.ArticleNumber == saleEventDTO.ArticleSoldNumber);

            if (articleInDatabase == null)
                throw new NullReferenceException(nameof(articleInDatabase));

            var newSaleEvent = new SaleEvent
            {
                TimeStamp = DateTime.Now,
                ArticleSoldNumber = saleEventDTO.ArticleSoldNumber,
                ArticleSoldPrice = saleEventDTO.ArticleSoldPrice,
                ArticleSold = articleInDatabase
            };

            this.context.SaleEvents.Add(newSaleEvent);
            this.context.SaveChanges();

            return newSaleEvent;
        }

        /// <summary>
        /// Gets the sold articles per day.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>
        /// Returns number of sold articles for given date.
        /// </returns>
        public int GetSoldArticlesPerDay(DateTime? date)
        {
            this.ValidateDataPresenceInTheDatabase();

            if (date.HasValue)
            {
                //Return sold articles for that day
                return this.context.SaleEvents
                    .Where(a => a.TimeStamp.Date == date.Value)
                    .Select(s => s.ArticleSoldNumber)?.Distinct().Count() ?? 0;
            }
            else
            {
                //Return sold articles for today
                return this.context.SaleEvents
                    .Where(a => a.TimeStamp.Date == DateTime.Now.Date)
                    .Select(s => s.ArticleSoldNumber)?.Distinct().Count() ?? 0;
            }
        }

        /// <summary>
        /// Gets the total revenue per day.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>
        /// Returns amount of total revenue for given date.
        /// </returns>
        public double GetTotalRevenuePerDay(DateTime? date)
        {
            this.ValidateDataPresenceInTheDatabase();

            if (date.HasValue)
            {
                //Return revenue for that day
                return this.context.SaleEvents.Include(s => s.ArticleSold)
                    .Where(a => a.TimeStamp.Date == date.Value)
                    .Select(s => s.ArticleSoldPrice)?.Sum() ?? 0;
            }
            else
            {
                //Return revenue for today
                return this.context.SaleEvents
                    .Where(a => a.TimeStamp.Date == DateTime.Now.Date)
                    .Select(s => s.ArticleSoldPrice)?.Sum() ?? 0;
            }
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
            this.ValidateDataPresenceInTheDatabase();

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

            return sales
                .GroupBy(a => a.ArticleSoldNumber)
                .Select(g => new Tuple<string, double>(g.Key, g.Sum(g => g.ArticleSoldPrice)))
                .OrderByDescending(a => a.Item2)
                .ToList();
        }

        /// <summary>
        /// Validates the data presence in the database.
        /// </summary>
        /// <exception cref="System.NullReferenceException">
        /// SaleEvents
        /// or
        /// Articles
        /// </exception>
        private void ValidateDataPresenceInTheDatabase()
        {
            if (this.context.SaleEvents == null)
                throw new NullReferenceException(nameof(this.context.SaleEvents));

            if (this.context.Articles == null)
                throw new NullReferenceException(nameof(this.context.Articles));
        }
    }
}
