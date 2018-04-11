using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace eBike.Data.Entities
{
    [Table("ShoppingCart")]
    public partial class ShoppingCart
    {
        public ShoppingCart()
        {
            ShoppingCartItems = new HashSet<ShoppingCartItem>();
        }
        [Key]
        public int ShoppingCartID { get; set; }

        [Required(ErrorMessage = "OnlineCustomerID is Required")]
        public int OnlineCustomerID { get; set; }

        [Required(ErrorMessage = "CreatedOn is Required")]
        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public virtual OnlineCustomer OnlineCustomer { get; set; }
        public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}