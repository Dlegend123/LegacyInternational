namespace LegacyInternational.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Serializable]
    [Table("flightlist")]
    public partial class flightlist
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public flightlist()
        {
            bookflights = new HashSet<bookflight>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int flight_id { get; set; }

        public int airline_id { get; set; }

        public int departure_airport_id { get; set; }

        public int arrival_airport_id { get; set; }

        public string departure_datetime { get; set; }

        public string arrival_datetime { get; set; }

        [StringLength(50)]
        public string seat_num { get; set; }

        [StringLength(50)]
        public string cost { get; set; }

        public string date_created { get; set; }

        public virtual airlinelist airlinelist { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<bookflight> bookflights { get; set; }
    }
}
