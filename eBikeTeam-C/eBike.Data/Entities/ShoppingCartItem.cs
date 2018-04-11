using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace eBike.Data.Entities
{
    [Table("ShoppingCartItem")]
    public partial class ShoppingCartItem
    {
        [Key]
        public int ShoppingCartItemID { get; set; }

        [Required(ErrorMessage = "ShoppingCartID is Required")]
        public int ShoppingCartID { get; set; }

        [Required(ErrorMessage = "PartID is Required")]
        public int PartID { get; set; }

        [Required(ErrorMessage = " Quantity is Required")]
        public int Quantity { get; set; }

        public virtual Part Part { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }
    }
}