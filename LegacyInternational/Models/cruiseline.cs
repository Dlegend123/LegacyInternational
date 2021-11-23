namespace LegacyInternational.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cruiseline")]
    public partial class cruiseline
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public cruiseline()
        {
            cruiselists = new HashSet<cruiselist>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int cruiseline_id { get; set; }

        [Required]
        public string cruiseline_name { get; set; }

        public string logo_path { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cruiselist> cruiselists { get; set; }
    }
}
