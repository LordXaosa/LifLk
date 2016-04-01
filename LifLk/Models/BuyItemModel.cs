using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LifLk.Models
{
    public class BuyItemModel
    {
        public objects_types ObjectsTypes { get; set; }
        public long ObjectId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        [Required]
        [Range(1,99)]
        public int Quality { get; set; }

        public bool ConfirmBuy { get; set; }

        public long GoldPrice
        {
            get { return (long)(Price/100/100/100); }
        }

        public long SilverPrice
        {
            get { return (long)((Price - GoldPrice*100*100*100)/100/100); }
        }

        public decimal CopperPrice
        {
            get
            {
                return (Price%10000)/100;
            }
        }

        public decimal Price { get; set; }
    }
}