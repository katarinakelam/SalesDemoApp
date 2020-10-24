using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DemoSalesApp.ViewModels.DTOs
{
    /// <summary>
    /// The sales event data transfer object.
    /// </summary>
    public class SaleEventDTO
    {
        [Required]
        public string ArticleSoldNumber { get; set; }

        [Required]
        [DefaultValue(0)]
        public double ArticleSoldPrice { get; set; }
    }
}
