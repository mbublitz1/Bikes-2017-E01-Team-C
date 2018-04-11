using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace eBike.Data.Entities
{
    public partial class Coupon
    {
        public Coupon()
        {
            JobDetails = new HashSet<JobDetail>();
            Sales = new HashSet<Sale>();
        }

        [Key]
        public int CouponID { get; set; }

        [Required(ErrorMessage = "CouponIDValue is Required")]
        [StringLength(10, ErrorMessage = "CouponIDValue limit is 10 characters ")]
        public string CouponIDValue { get; set; }

        [Required(ErrorMessage = "StartDate is Required")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "EndDate is Required")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "CouponDiscount is Required")]
        public int CouponDiscount { get; set; }

        [Required(ErrorMessage = "SalesOrService  is Required")]
        public int SalesOrService { get; set; }

        public virtual ICollection<JobDetail> JobDetails { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
    }
}