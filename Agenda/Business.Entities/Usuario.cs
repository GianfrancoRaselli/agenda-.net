namespace Business.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("usuarios")]
    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            Eventos = new HashSet<Evento>();
        }

        [Key]
        public int id_usuario { get; set; }

        [Required]
        [StringLength(50)]
        public string nombre_usuario { get; set; }

        [Required]
        [StringLength(50)]
        public string contrasenia { get; set; }

        [Required]
        [StringLength(50)]
        public string nombre_apellido { get; set; }

        [StringLength(50)]
        public string telefono { get; set; }

        [Required]
        [StringLength(50)]
        public string email { get; set; }

        [StringLength(50)]
        public string foto { get; set; }

        public int id_zona_horaria { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Evento> Eventos { get; set; }

        public virtual ZonaHoraria zonaHoraria { get; set; }
    }
}
