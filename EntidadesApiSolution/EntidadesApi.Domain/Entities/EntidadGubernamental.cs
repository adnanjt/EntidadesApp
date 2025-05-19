using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntidadesApi.Domain.Entities
{
    public class EntidadGubernamental
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(20)]
        public string Siglas { get; set; }

        [StringLength(50)]
        public string TipoEntidad { get; set; }
        //public TipoEntidad TipoEntidad { get; set; }

        [StringLength(500)]
        public string Descripcion { get; set; }

        [StringLength(100)]
        [Url]
        public string PaginaWeb { get; set; }

        [StringLength(20)]
        public string Telefono { get; set; }

        [StringLength(100)]
        [EmailAddress]
        public string CorreoElectronico { get; set; }

        [StringLength(200)]
        public string Direccion { get; set; }

        [StringLength(100)]
        public string Dependencia { get; set; }

        [StringLength(50)]
        public string Sector { get; set; }
        //public Sector Sector { get; set; }

        public bool Activo { get; set; } = true;

        public DateTime FechaCreacion { get; set; }

        // Campos de auditoría
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public DateTime? FechaModificacion { get; set; }
    }
}
