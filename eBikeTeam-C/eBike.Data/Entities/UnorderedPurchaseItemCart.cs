using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace eBike.Data.Entities
{
    public partial class UnorderedPurchaseItemCart
    {
        [Key]
        public int CartID { get; set; }

        [Required(ErrorMessage = "PurchaseOrderNumber is Required")]
        public int PurchaseOrderNumber { get; set; }

        [StringLength(100, ErrorMessage = "Description Length limit is 100 characters ")]
        public string Description { get; set; }

        [Required(ErrorMessage = "VendorPartNumber is Required")]
        public string VendorPartNumber { get; set; }

        [Required(ErrorMessage = "Quantity is Required")]
        public int Quantity { get; set; }
    }
}