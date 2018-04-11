using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace eBike.Data.Entities
{
    public partial class PurchaseOrder
    {
        public PurchaseOrder()
        {
            PurchaseOrderDetails = new HashSet<PurchaseOrderDetail>();
            ReceiveOrders = new HashSet<ReceiveOrder>();
        }

        [Key]
        public int PurchaseOrderID { get; set; }

        public int? PurchaseOrderNumber { get; set; }

        public DateTime? OrderDate { get; set; }

        [Required(ErrorMessage = "TaxAmount is Required")]
        public decimal TaxAmount { get; set; }

        [Required(ErrorMessage = "SubTotal is Required")]
        public decimal SubTotal { get; set; }

        [Required(ErrorMessage = "Closed is Required")]
        public bool Closed { get; set; }

        [StringLength(100, ErrorMessage = "Notes Length limit is 100 characters ")]
        public string Notes { get; set; }

        [Required(ErrorMessage = "EmployeeID is Required")]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = " VendorID is Required")]
        public int VendorID { get; set; }

        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual ICollection<ReceiveOrder> ReceiveOrders { get; set; }
    }
}