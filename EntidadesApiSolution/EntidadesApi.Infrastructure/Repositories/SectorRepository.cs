using EntidadesApi.Domain.Entities;
using EntidadesApi.Domain.Interfaces;

namespace EntidadesApi.Infrastructure.Repositories
{
    public class SectorRepository :Repository<Sector>, ISectorRepository
    {
        // Simulating an in-memory data store for Sectors.
        public SectorRepository(string filePath) : base(filePath)
        {
        }
    }
}
