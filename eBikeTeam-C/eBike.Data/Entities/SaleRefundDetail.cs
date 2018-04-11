using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace eBike.Data.Entities
{
    public partial class SaleRefundDetail
    {
        [Key]
        public int SaleRefundDetailID { get; set; }

        [Required(ErrorMessage = "SaleRefundID is Required")]
        public int SaleRefundID { get; set; }

        [Required(ErrorMessage = "PartID is Required")]
        public int PartID { get; set; }

        [Required(ErrorMessage = "Quantity is Required")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "SellingPrice is Required")]
        public decimal SellingPrice { get; set; }

        [StringLength(150, ErrorMessage = "Reason Length limit is 150 characters ")]
        public string Reason { get; set; }

        public virtual Part Part { get; set; }
        public virtual SaleRefund SaleRefund { get; set; }
    }
}