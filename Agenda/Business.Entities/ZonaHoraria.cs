namespace Business.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("zonas_horarias")]
    public partial class ZonaHoraria
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ZonaHoraria()
        {
            Usuarios = new HashSet<Usuario>();
        }

        [Key]
        public int id_zona_horaria { get; set; }

        [Required]
        [StringLength(50)]
        public string descripcion { get; set; }

        public float diferencia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
