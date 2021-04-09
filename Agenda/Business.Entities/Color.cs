namespace Business.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("colores")]
    public partial class Color
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Color()
        {
            Eventos = new HashSet<Evento>();
        }

        [Key]
        public int id_color { get; set; }

        [Required]
        [StringLength(50)]
        public string descripcion { get; set; }

        [Required]
        [StringLength(10)]
        public string codigo_hex { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Evento> Eventos { get; set; }
    }
}
