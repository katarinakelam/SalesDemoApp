using System;
using System.Collections.Generic;
using System.Text;

namespace DemoSalesApp.ViewModels.ViewModels
{
    /// <summary>
    /// The sale event view model.
    /// </summary>
    public class SaleEventViewModel
    {
        public int Id { get; set; }

        public DateTime TimeStamp { get; set; }

        public string ArticleSoldNumber { get; set; }

        public double ArticleSoldPrice { get; set; }
    }
}
