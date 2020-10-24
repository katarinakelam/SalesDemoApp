using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DemoSalesApp.Models
{
    /// <summary>
    /// The article model.
    /// </summary>
    public class Article
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(32, MinimumLength = 1, ErrorMessage = "Article number can have up to 32 characters.")]
        public string ArticleNumber { get; set; }

        public string Name { get; set; }
    }
}
