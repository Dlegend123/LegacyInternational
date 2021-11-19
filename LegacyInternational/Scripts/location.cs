namespace LegacyInternational.Scripts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("location")]
    public partial class location
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public location()
        {
            airportlists = new HashSet<airportlist>();
            portlists = new HashSet<portlist>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int location_id { get; set; }

        [Required]
        public string city { get; set; }

        [Required]
        public string country { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<airportlist> airportlists { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<portlist> portlists { get; set; }
    }
}
