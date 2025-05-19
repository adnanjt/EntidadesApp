using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesApi.Application.DTOs
{
    public class EntidadGubernamentalDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Siglas { get; set; }
        public Guid TipoEntidadId { get; set; }
        public string TipoEntidadNombre { get; set; }
        public string Descripcion { get; set; }
        public string PaginaWeb { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public string Direccion { get; set; }
        public string Dependencia { get; set; }
        public Guid SectorId { get; set; }
        public string SectorNombre { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
