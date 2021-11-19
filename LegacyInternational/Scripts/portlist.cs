namespace LegacyInternational.Scripts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("portlist")]
    public partial class portlist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int port_id { get; set; }

        [Required]
        public string port_name { get; set; }

        public int location_id { get; set; }

        public virtual location location { get; set; }
    }
}
