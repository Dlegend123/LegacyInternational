namespace LegacyInternational.Scripts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("airportlist")]
    public partial class airportlist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int airport_id { get; set; }

        [Required]
        public string airport { get; set; }

        public int location_id { get; set; }

        public virtual location location { get; set; }
    }
}
