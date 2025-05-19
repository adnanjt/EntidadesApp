using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesApi.Application.DTOs
{
    public class EntidadGubernamentalCreateDto
    {
        public string Nombre { get; set; }
        public string Siglas { get; set; }
        public string TipoEntidadId { get; set; }
        public string Descripcion { get; set; }
        public string PaginaWeb { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public string Direccion { get; set; }
        public string Dependencia { get; set; }
        public string SectorId { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}
