using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace eBike.Data.Entities
{
    public partial class JobDetail
    {
        public JobDetail()
        {
            JobDetailParts = new HashSet<JobDetailPart>();
        }

        [Key]
        public int JobDetailID { get; set; }

        [Required(ErrorMessage = "JobID is Required")]
        public int JobID { get; set; }

        [Required(ErrorMessage = "Description is Required")]
        [StringLength(100, ErrorMessage = "Description limit is 100 characters ")]
        public string Description { get; set; }

        [Required(ErrorMessage = "JobHours is Required")]
        public decimal JobHours { get; set; }

        public string Comments { get; set; }

        public int? CouponID { get; set; }

        public bool? Completed { get; set; }

        public virtual ICollection<JobDetailPart> JobDetailParts { get; set; }
        public virtual Coupon Coupon { get; set; }
        public virtual Job Job { get; set; }
    }
}