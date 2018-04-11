using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace eBike.Data.Entities
{
    public partial class JobDetailPart
    {
        public JobDetailPart()
        {
        }

        [Key]
        public int JobDetailPartID { get; set; }

        [Required(ErrorMessage = "JobDetailID is Required")]
        public int JobDetailID { get; set; }

        public int PartID { get; set; }

        [Required(ErrorMessage = "Quantity is Required")]
        public short Quantity { get; set; }

        [Required(ErrorMessage = "SellingPrice is Required")]
        public decimal SellingPrice { get; set; }

        public virtual JobDetail JobDetail { get; set; }
        public virtual Part Part { get; set; }
    }
}