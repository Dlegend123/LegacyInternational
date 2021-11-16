namespace LegacyInternational
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cruiseroom")]
    public partial class cruiseroom
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public cruiseroom()
        {
            bookcruises = new HashSet<bookcruise>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int room_num { get; set; }

        public string type { get; set; }

        public int? cruise_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<bookcruise> bookcruises { get; set; }

        public virtual cruiselist cruiselist { get; set; }
    }
}
