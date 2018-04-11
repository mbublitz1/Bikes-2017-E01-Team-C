using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace eBike.Data.Entities
{
    public partial class ReceiveOrder
    {
        public ReceiveOrder()
        {
            ReceiveOrderDetails = new HashSet<ReceiveOrderDetail>();
            ReturnedOrderDetails = new HashSet<ReturnedOrderDetail>();
        }
        [Key]
        public int ReceiveOrderID { get; set; }

        [Required(ErrorMessage = "PurchaseOrderID is Required")]
        public int PurchaseOrderID { get; set; }

        public DateTime? ReceiveDate { get; set; }

        public virtual ICollection<ReceiveOrderDetail> ReceiveOrderDetails { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
        public virtual ICollection<ReturnedOrderDetail> ReturnedOrderDetails { get; set; }
    }
}