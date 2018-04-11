using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace eBike.Data.Entities
{
    public partial class Category
    {
        public Category()
        {
            Parts = new HashSet<Part>();
        }
        [Key]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Description is Required")]
        [StringLength(40, ErrorMessage = "Description Length limit is 40 characters ")]
        public string Description { get; set; }

        public virtual ICollection<Part> Parts { get; set; }
    }
}