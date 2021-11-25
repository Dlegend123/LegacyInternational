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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int booking_id { get; set; }

        public int flight_id { get; set; }

        [Required]
        [StringLength(50)]
        public string username { get; set; }

        [Required]
        [StringLength(50)]
        public string address { get; set; }

        public string email { get; set; }

        public string dob { get; set; }

        [StringLength(50)]
        public string seat_num { get; set; }

        public int? num_of_adults { get; set; }

        public virtual flightlist flightlist { get; set; }

        public virtual user user { get; set; }
    }
}
