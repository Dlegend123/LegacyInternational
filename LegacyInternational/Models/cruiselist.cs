namespace LegacyInternational.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Serializable]
    [Table("cruiselist")]
    public partial class cruiselist
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public cruiselist()
        {
            bookcruises = new HashSet<bookcruise>();
            cruiserooms = new HashSet<cruiseroom>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int cruise_id { get; set; }

        public int? cruiseline_id { get; set; }

        public int? departure_port_id { get; set; }

        public string start_datetime { get; set; }

        public string end_datetime { get; set; }

        [StringLength(10)]
        public string cost { get; set; }

        [StringLength(10)]
        public string cruise_length { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<bookcruise> bookcruises { get; set; }

        public virtual cruiseline cruiseline { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cruiseroom> cruiserooms { get; set; }
    }
}
