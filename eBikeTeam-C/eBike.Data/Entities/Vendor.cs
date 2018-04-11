using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace eBike.Data.Entities
{
    public partial class Vendor
    {
        public Vendor()
        {
            Parts = new HashSet<Part>();
            PurchaseOrders = new HashSet<PurchaseOrder>();
        }
        [Key]
        public int VendorID { get; set; }

        [Required(ErrorMessage = "VendorName is Required")]
        [StringLength(100, ErrorMessage = "VendorName Length limit is 100 characters ")]
        public string VendorName { get; set; }

        [Required(ErrorMessage = "Phone is Required")]
        [StringLength(12, ErrorMessage = "Phone Length limit is 12 characters ")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Address is Required")]
        [StringLength(30, ErrorMessage = "Address Length limit is 30 characters ")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is Required")]
        [StringLength(30, ErrorMessage = "City Length limit is 30 characters ")]
        public string City { get; set; }

        [Required(ErrorMessage = "ProvinceID is Required")]
        [StringLength(2, ErrorMessage = "ProvinceID Length limit is 2 characters ")]
        public string ProvinceID { get; set; }

        [Required(ErrorMessage = "PostalCode is Required")]
        [StringLength(6, ErrorMessage = "PostalCode Length limit is 6 characters ")]
        public string PostalCode { get; set; }

        public virtual ICollection<Part> Parts { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
    }
}