using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesApi.Application.DTOs
{
    public class PaginationDto
    {
        private int _pageNumber = 1;
        private int _pageSize = 10;
        private const int MaxPageSize = 50;

        public int PageNumber
        {
            get => _pageNumber;
            set => _pageNumber = (value < 1) ? 1 : value;
        }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : (value < 1) ? 1 : value;
        }
    }

    public class PagedResponse<T> where T : class
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public bool HasPrevious => PageNumber > 1;
        public bool HasNext => PageNumber < TotalPages;
        public T[] Data { get; set; }

        public PagedResponse(T[] data, int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = count;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Data = data;
        }
    }

    public class EntidadGubernamentalFilterDto : PaginationDto
    {
        public string? SearchTerm { get; set; }
        public Guid? SectorId { get; set; }
        public Guid? TipoEntidadId { get; set; }
        public bool? Activo { get; set; }
        public DateTime? FechaCreacionDesde { get; set; }
        public DateTime? FechaCreacionHasta { get; set; }
        public string? OrderBy { get; set; } = "Nombre";
        public bool OrderByDescending { get; set; } = false;
    }
}
