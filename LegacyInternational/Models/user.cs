namespace LegacyInternational.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("user")]
    public partial class user
    {
        [Key]
        [StringLength(50)]
        public string username { get; set; }

        [Required]
        public string first_name { get; set; }

        public string last_name { get; set; }

        [StringLength(50)]
        public string dob { get; set; }

        [StringLength(50)]
        public string address { get; set; }

        [StringLength(10)]
        public string contact_num { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        public byte[] ProfilePic { get; set; }
    }
}
