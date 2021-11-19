namespace LegacyInternational.Scripts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("airlinelist")]
    public partial class airlinelist
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public airlinelist()
        {
            flightlists = new HashSet<flightlist>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int airline_id { get; set; }

        [Required]
        public string airline_name { get; set; }

        public string logo_path { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<flightlist> flightlists { get; set; }
    }
}
