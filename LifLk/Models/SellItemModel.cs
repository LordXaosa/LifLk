using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LifLk.Models
{
    public class SellItemModel
    {
        public int ItemId { get; set; }
        public items Item { get; set; }
        [Range(1,int.MaxValue)]
        [Required]
        public int Quantity { get; set; }
        public bool Confirm { get; set; }
        public long GoldPrice
        {
            get { return (long)(Price / 100 / 100 / 100); }
        }

        public long SilverPrice
        {
            get { return (long)((Price - GoldPrice * 100 * 100 * 100) / 100 / 100); }
        }

        public decimal CopperPrice
        {
            get
            {
                return (Price % 10000) / 100;
            }
        }

        public decimal Price { get; set; }
    }
}