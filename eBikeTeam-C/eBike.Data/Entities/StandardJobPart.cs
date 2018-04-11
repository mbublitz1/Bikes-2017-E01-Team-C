using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace eBike.Data.Entities
{
    public partial class StandardJobPart
    {
        [Key]
        public int StandardJobPartID { get; set; }

        [Required(ErrorMessage = "StandardJobID is Required")]
        public int StandardJobID { get; set; }

        [Required(ErrorMessage = "PartID is Required")]
        public int PartID { get; set; }

        [Required(ErrorMessage = "Quantity is Required")]
        public int Quantity { get; set; }

        public virtual Part Part { get; set; }
        public virtual StandardJob StandardJob { get; set; }
    }
}