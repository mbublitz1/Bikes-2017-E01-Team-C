using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace eBike.Data.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Jobs = new HashSet<Job>();
        }

        [Key]
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "LastName is Required")]
        [StringLength(30, ErrorMessage = "LastName limit is 30 characters ")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "FirstName is Required")]
        [StringLength(30, ErrorMessage = "FirstName limit is 30 characters ")]
        public string FirstName { get; set; }

        [StringLength(40, ErrorMessage = "Address limit is 40 characters ")]
        public string Address { get; set; }

        [StringLength(20, ErrorMessage = "City limit is 20 characters ")]
        public string City { get; set; }

        [StringLength(2, ErrorMessage = "Province limit is 2 characters ")]
        public string Province { get; set; }

        [StringLength(6, ErrorMessage = "Postal Code limit is 6 characters ")]
        [RegularExpression("[a-zA-Z][0-9][a-zA-Z][0-9][a-zA-Z][0-9]", ErrorMessage= "Must be in proper PostalCode Format A0A 0A0")]
        public string PostalCode { get; set; }

        [StringLength(12, ErrorMessage = "ContactPhone limit is 12 characters ")]
        [Phone(ErrorMessage = "You must enter a valid phone number")]
        public string ContactPhone { get; set; }

        [StringLength(30, ErrorMessage = "EmailAddress  limit is 30 characters ")]
        [EmailAddress(ErrorMessage = "Your email address must be valid.")]
        public string EmailAddress { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }
    }
}