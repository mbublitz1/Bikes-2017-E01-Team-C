using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace eBike.Data.Entities
{
    public partial class ReceiveOrderDetail
    {
        public ReceiveOrderDetail()
        {
        }
        [Key]
        public int ReceiveOrderDetailID { get; set; }

        [Required(ErrorMessage = "ReceiveOrderID is Required")]
        public int ReceiveOrderID { get; set; }

        [Required(ErrorMessage = "PurchaseOrderDetailID is Required")]
        public int PurchaseOrderDetailID { get; set; }

        [Required(ErrorMessage = "QuantityReceived is Required")]
        public int QuantityReceived { get; set; }

        public virtual PurchaseOrderDetail PurchaseOrderDetail { get; set; }
        public virtual ReceiveOrder ReceiveOrder { get; set; }
    }
}