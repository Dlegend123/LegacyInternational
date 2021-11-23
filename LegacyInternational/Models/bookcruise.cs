namespace LegacyInternational
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("bookcruise")]
    public partial class bookcruise
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int booking_id { get; set; }

        public int? cruise_id { get; set; }

        [StringLength(50)]
        public string username { get; set; }

        [StringLength(50)]
        public string check_in_date { get; set; }

        [StringLength(50)]
        public string check_out_date { get; set; }

        public int? room_num { get; set; }

        public int? num_of_adults { get; set; }

        public virtual cruiselist cruiselist { get; set; }

        public virtual cruiseroom cruiseroom { get; set; }

        public virtual user user { get; set; }
    }
}
