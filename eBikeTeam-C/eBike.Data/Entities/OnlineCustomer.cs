using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace eBike.Data.Entities
{
    [Table("OnlineCustomer")]
    public partial class OnlineCustomer
    {
        public OnlineCustomer()
        {
            ShoppingCarts = new HashSet<ShoppingCart>();
        }

        [Key]
        public int OnlineCustomerID { get; set; }

        [StringLength(128,ErrorMessage = "UserName length limit is 128 characters")]
        public string UserName { get; set; }

        public Guid? TrackingCookie { get; set; }

        [Required(ErrorMessage = "CreatedOn is Required")]
        public DateTime CreatedOn { get; set; }

        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}