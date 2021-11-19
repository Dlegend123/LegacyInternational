namespace LegacyInternational.Scripts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("admin")]
    public partial class admin
    {
        [Key]
        [StringLength(10)]
        public string username { get; set; }

        public string password { get; set; }

        public string first_name { get; set; }

        public string last_name { get; set; }
    }
}
