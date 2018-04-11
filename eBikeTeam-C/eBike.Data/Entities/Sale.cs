using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace eBike.Data.Entities
{
    public partial class Sale
    {
        public Sale()
        {
            SaleDetails = new HashSet<SaleDetail>();
            SaleRefunds = new HashSet<SaleRefund>();
        }

        [Key]
        public int SaleID { get; set; }

        [Required(ErrorMessage = "SaleDate is Required")]
        public DateTime SaleDate { get; set; }

        [StringLength(128, ErrorMessage = "UserName Length limit is 128 characters ")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "EmployeeID is Required")]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "TaxAmount is Required")]
        public decimal TaxAmount { get; set; }

        [Required(ErrorMessage = "SubTotal is Required")]
        public decimal SubTotal { get; set; }

        public int? CouponID { get; set; }

        [Required(ErrorMessage = "PaymentType is Required")]
        [StringLength(1, ErrorMessage = "PaymentType Length limit is 1 characters ")]
        public string PaymentType { get; set; }

        public Guid? PaymentToken { get; set; }

        public int? JobID { get; set; }

        public virtual ICollection<SaleDetail> SaleDetails { get; set; }
        public virtual ICollection<SaleRefund> SaleRefunds { get; set; }
        public virtual Coupon Coupon { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Job Job { get; set; }
    }
}