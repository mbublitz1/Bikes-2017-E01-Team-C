using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace eBike.Data.Entities
{
    public partial class Position
    {
        public Position()
        {
            Employees = new HashSet<Employee>();
        }

        [Key]
        public int PositionID { get; set; }

        [Required(ErrorMessage = "Description is Required")]
        [StringLength(40, ErrorMessage = "Description Length limit is 40 characters ")]
        public string Description { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}