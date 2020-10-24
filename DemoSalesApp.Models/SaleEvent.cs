using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DemoSalesApp.Models
{
    /// <summary>
    /// The sales event model.
    /// </summary>
    public class SaleEvent
    {
        public int Id { get; set; }

        public DateTime TimeStamp { get; set; }

        public virtual Article ArticleSold { get; set; }

        [ForeignKey("ArticleSold")]
        public string ArticleSoldNumber { get; set; }

        public double ArticleSoldPrice { get; set; }
    }
}
