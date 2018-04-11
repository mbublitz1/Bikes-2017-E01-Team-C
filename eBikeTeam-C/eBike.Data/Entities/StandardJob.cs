using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace eBike.Data.Entities
{
    public partial class StandardJob
    {
        public StandardJob()
        {
            StandardJobParts = new HashSet<StandardJobPart>();
        }
        [Key]
        public int StandardJobID { get; set; }

        [Required(ErrorMessage = "Description is Required")]
        [StringLength(100, ErrorMessage = "Description Length limit is 100 characters ")]
        public string Description { get; set; }

        [Required(ErrorMessage = "StandardHours is Required")]
        public decimal StandardHours { get; set; }

        public virtual ICollection<StandardJobPart> StandardJobParts { get; set; }
    }
}