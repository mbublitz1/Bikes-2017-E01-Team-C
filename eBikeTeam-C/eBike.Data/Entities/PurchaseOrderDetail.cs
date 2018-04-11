using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace eBike.Data.Entities
{
    public partial class PurchaseOrderDetail
    {
        public PurchaseOrderDetail()
        {
            ReceiveOrderDetails = new HashSet<ReceiveOrderDetail>();
            ReturnedOrderDetails = new HashSet<ReturnedOrderDetail>();
        }

        [Key]
        public int PurchaseOrderDetailID { get; set; }

        [Required(ErrorMessage = "PurchaseOrderID is Required")]
        public int PurchaseOrderID { get; set; }

        [Required(ErrorMessage = "PartID is Required")]
        public int PartID { get; set; }

        [Required(ErrorMessage = "Quantity is Required")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "PurchasePrice is Required")]
        public decimal PurchasePrice { get; set; }

        [StringLength(50, ErrorMessage = "VendorPartNumber Length limit is 50 characters ")]
        public string VendorPartNumber { get; set; }

        public virtual Part Part { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
        public virtual ICollection<ReceiveOrderDetail> ReceiveOrderDetails { get; set; }
        public virtual ICollection<ReturnedOrderDetail> ReturnedOrderDetails { get; set; }
    }
}