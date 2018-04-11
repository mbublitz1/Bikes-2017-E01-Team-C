using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace eBike.Data.Entities
{
    public partial class SaleDetail
    {
        [Key]
        public int SaleDetailID { get; set; }

        [Required(ErrorMessage = "SaleID is Required")]
        public int SaleID { get; set; }

        [Required(ErrorMessage = "PartID is Required")]
        public int PartID { get; set; }

        [Required(ErrorMessage = "Quantity is Required")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "SellingPrice is Required")]
        public decimal SellingPrice { get; set; }

        [Required(ErrorMessage = "Backordered is Required")]
        public bool Backordered { get; set; }

        public DateTime? ShippedDate { get; set; }

        public virtual Part Part { get; set; }
        public virtual Sale Sale { get; set; }
    }
}