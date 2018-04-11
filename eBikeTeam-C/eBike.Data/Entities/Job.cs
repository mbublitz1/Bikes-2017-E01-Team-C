using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace eBike.Data.Entities
{
    public partial class Job
    {
        public Job()
        {
            JobDetails = new HashSet<JobDetail>();
            Sales = new HashSet<Sale>();
        }

        [Key]
        public int JobID { get; set; }

        [Required(ErrorMessage = "JobDateIn is Required")]
        public DateTime JobDateIn { get; set; }

        public DateTime? JobDateStarted { get; set; }

        public DateTime? JobDateDone { get; set; }

        public DateTime? JobDateOut { get; set; }

        [Required(ErrorMessage = "CustomerID is Required")]
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "EmployeeID is Required")]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "ShopRate is Required")]
        public decimal ShopRate { get; set; }

        [Required(ErrorMessage = "StatusCode is Required")]
        [StringLength(1, ErrorMessage = "StatusCode limit is 1 characters ")]
        public string StatusCode { get; set; }

        [Required(ErrorMessage = "VehicleIdentification is Required")]
        [StringLength(50, ErrorMessage = "VehicleIdentification limit is 50 characters ")]
        public string VehicleIdentification { get; set; }

        public virtual ICollection<JobDetail> JobDetails { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
    }
}