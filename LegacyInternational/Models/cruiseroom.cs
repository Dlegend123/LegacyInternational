namespace LegacyInternational.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cruiseroom")]
    public partial class cruiseroom
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int room_num { get; set; }

        public string type { get; set; }

        public int? cruise_id { get; set; }

        public int? num_of_adults { get; set; }

        public virtual cruiselist cruiselist { get; set; }
    }
}
