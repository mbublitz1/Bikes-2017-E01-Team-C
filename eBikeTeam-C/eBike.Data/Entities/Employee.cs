using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace eBike.Data.Entities
{
    public partial class Employee
    {
        public Employee()
        {
            Jobs = new HashSet<Job>();
            PurchaseOrders = new HashSet<PurchaseOrder>();
            SaleRefunds = new HashSet<SaleRefund>();
            Sales = new HashSet<Sale>();
        }

        [Key]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Social Insurance Number is Required")]
        [StringLength(9, ErrorMessage = "Social Insurance Number limit is 9 characters ")]
        public string SocialInsuranceNumber { get; set; }

        [Required(ErrorMessage = "Employee Last Name is Required")]
        [StringLength(30, ErrorMessage = "LastName limit is 30 characters ")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Employee First Name is Required")]
        [StringLength(30, ErrorMessage = "FirstName limit is 30 characters ")]
        public string FirstName { get; set; }

        [StringLength(40, ErrorMessage = "Address limit is 40 characters ")]
        public string Address { get; set; }

        [StringLength(20, ErrorMessage = "City limit is 20 characters ")]
        public string City { get; set; }

        [StringLength(2, ErrorMessage = "Province limit is 2 characters ")]
        public string Province { get; set; }

        [StringLength(6, ErrorMessage = "PostalCode limit is 6 characters ")]
        [RegularExpression("[a-zA-Z][0-9][a-zA-Z][0-9][a-zA-Z][0-9]", ErrorMessage = "Postal Code must be in the propper format. Eg. A0A0A0")]
        public string PostalCode { get; set; }

        [StringLength(12, ErrorMessage = "HomePhone limit is 12 characters ")]
        [Phone(ErrorMessage = "Phone Number must be in the proper format")]
        public string HomePhone { get; set; }

        [StringLength(30, ErrorMessage = "EmailAddress limit is 30 characters ")]
        [EmailAddress(ErrorMessage = "Email address must be in a valid format")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "PositionID is Required")]
        public int PositionID { get; set; }

        public virtual Position Position { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual ICollection<SaleRefund> SaleRefunds { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
    }
}