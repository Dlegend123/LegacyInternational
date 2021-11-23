namespace LegacyInternational.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("bookflight")]
    public partial class bookflight
    {
        [Key]
        public int booking_id { get; set; }

        public int flight_id { get; set; }

        [Required]
        [StringLength(50)]
        public string username { get; set; }

        public string email { get; set; }

        public string dob { get; set; }

        [StringLength(50)]
        public string seat_num { get; set; }

        public int num_of_adults { get; set; }

        [StringLength(50)]
        public string date { get; set; }
    }
}
