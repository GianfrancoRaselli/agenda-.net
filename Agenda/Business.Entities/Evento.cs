namespace Business.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("eventos")]
    public partial class Evento
    {
        [Key]
        public int id_evento { get; set; }

        [Required]
        [StringLength(50)]
        public string nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string descripcion { get; set; }

        public bool todo_el_dia { get; set; }

        public DateTime fecha_hora_evento { get; set; }

        [NotMapped]
        public String fecha_evento_string { get; set; }

        [NotMapped]
        public String fecha_evento_completa_string { get; set; }

        [NotMapped]
        public String hora_evento_string { get; set; }

        public DateTime? fecha_hora_recordatorio { get; set; }

        [NotMapped]
        public String fecha_hora_recordatorio_string { get; set; }

        [NotMapped]
        public bool recordatorio { get; set; }

        public bool? recordatorio_enviado { get; set; }

        public int id_usuario { get; set; }

        public int id_color { get; set; }

        public virtual Color color { get; set; }

        public virtual Usuario usuario { get; set; }
    }
}
