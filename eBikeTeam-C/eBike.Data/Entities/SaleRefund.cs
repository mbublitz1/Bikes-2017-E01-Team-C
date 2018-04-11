using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace eBike.Data.Entities
{
    public partial class SaleRefund
    {
        public SaleRefund()
        {

            SaleRefundDetails = new HashSet<SaleRefundDetail>();
        }
        [Key]
        public int SaleRefundID { get; set; }

        [Required(ErrorMessage = "SaleRefundDate is Required")]
        public DateTime SaleRefundDate { get; set; }

        [Required(ErrorMessage = "SaleID is Required")]
        public int SaleID { get; set; }

        [Required(ErrorMessage = "EmployeeID is Required")]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "TaxAmount is Required")]
        public decimal TaxAmount { get; set; }

        [Required(ErrorMessage = "SubTotal is Required")]
        public decimal SubTotal { get; set; }

        public virtual ICollection<SaleRefundDetail> SaleRefundDetails { get; set; }
        public virtual Sale Sale { get; set; }
        public virtual Employee Employee { get; set; }
    }
}