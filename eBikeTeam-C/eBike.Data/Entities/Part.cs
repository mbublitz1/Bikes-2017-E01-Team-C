using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace eBike.Data.Entities
{
    public partial class Part
    {
        public Part()
        {
            JobDetailParts = new HashSet<JobDetailPart>();
            PurchaseOrderDetails = new HashSet<PurchaseOrderDetail>();
            SaleDetails = new HashSet<SaleDetail>();
            SaleRefundDetails = new HashSet<SaleRefundDetail>();
            ShoppingCartItems = new HashSet<ShoppingCartItem>();
            StandardJobParts = new HashSet<StandardJobPart>();
        }

        [Key]
        public int PartID { get; set; }

        [Required(ErrorMessage = "Description is Required")]
        [StringLength(40, ErrorMessage = "Description Length limit is 40 characters ")]
        public string Description { get; set; }

        [Required(ErrorMessage = "PurchasePrice is Required")]
        public decimal PurchasePrice { get; set; }

        [Required(ErrorMessage = "SellingPrice is Required")]
        public decimal SellingPrice { get; set; }

        [Required(ErrorMessage = "QuantityOnHand is Required")]
        public int QuantityOnHand { get; set; }

        [Required(ErrorMessage = "ReorderLevel is Required")]
        public int ReorderLevel { get; set; }

        [Required(ErrorMessage = "QuantityOnOrder is Required")]
        public int QuantityOnOrder { get; set; }

        [Required(ErrorMessage = "CategoryID is Required")]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Refundable is Required")]
        [StringLength(1, ErrorMessage = "Refundable Length limit is 1 characters ")]
        public string Refundable { get; set; }

        [Required(ErrorMessage = "Discontinued is Required")]
        public bool Discontinued { get; set; }

        [Required(ErrorMessage = "VendorID is Required")]
        public int VendorID { get; set; }

        public virtual ICollection<JobDetailPart> JobDetailParts { get; set; }
        public virtual Category Category { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public virtual ICollection<SaleDetail> SaleDetails { get; set; }
        public virtual ICollection<SaleRefundDetail> SaleRefundDetails { get; set; }
        public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
        public virtual ICollection<StandardJobPart> StandardJobParts { get; set; }
    }
}