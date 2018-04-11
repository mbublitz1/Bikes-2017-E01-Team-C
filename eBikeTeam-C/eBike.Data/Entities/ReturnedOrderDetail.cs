using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace eBike.Data.Entities
{
    public partial class ReturnedOrderDetail
    {

        [Key]
        public int ReturnedOrderDetailID { get; set; }

        [Required(ErrorMessage = "ReceiveOrderID is Required")]
        public int ReceiveOrderID { get; set; }

        public int? PurchaseOrderDetailID { get; set; }

        [StringLength(50, ErrorMessage = "ItemDescription Length limit is 50 characters ")]
        public string ItemDescription { get; set; }

        [Required(ErrorMessage = "Quantity is Required")]
        public int Quantity { get; set; }

        [StringLength(50, ErrorMessage = "Reason Length limit is 50 characters ")]
        [Required(ErrorMessage = "Reason is Required")]
        public string Reason { get; set; }

        [StringLength(50, ErrorMessage = "VendorPartNumber Length limit is 50 characters ")]
        [Required(ErrorMessage = "VendorPartNumber is Required")]
        public string VendorPartNumber { get; set; }

        public virtual PurchaseOrderDetail PurchaseOrderDetail { get; set; }
        public virtual ReceiveOrder ReceiveOrder { get; set; }
    }
}