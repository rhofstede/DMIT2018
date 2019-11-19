namespace ChinookSystem.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            Invoices = new HashSet<Invoice>();
        }

        public int CustomerId { get; set; }

        [Required(ErrorMessage = "First name required.")]
        [StringLength(40)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name required.")]
        [StringLength(20)]
        public string LastName { get; set; }

        [StringLength(80)]
        public string Company { get; set; }

        [StringLength(70)]
        public string Address { get; set; }

        [StringLength(40)]
        public string City { get; set; }

        [StringLength(40, ErrorMessage ="State limited to 40 characters.")]
        public string State { get; set; }

        [StringLength(40, ErrorMessage ="Country limited to 40 characters.")]
        public string Country { get; set; }

        [StringLength(10, ErrorMessage ="Postal code limited to 10 characters.")]
        public string PostalCode { get; set; }

        [StringLength(24, ErrorMessage ="Phone number limited to 24 characters.")]
        public string Phone { get; set; }

        [StringLength(24, ErrorMessage ="Fax number limited to 24 characters.")]
        public string Fax { get; set; }

        [Required(ErrorMessage = "Email required.")]
        [StringLength(60, ErrorMessage ="Email limited to 60 characters.")]
        public string Email { get; set; }

        public int? SupportRepId { get; set; }

        public virtual Employee Employee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Invoice> Invoices { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}
