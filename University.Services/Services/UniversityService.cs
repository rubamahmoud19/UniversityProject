
using University.Data;
using University.Entity.Entities;

namespace University.Services.Services
{
    public class UniversityService : IUniversityService
    {
        private readonly UniversityDbContext _context;
        public UniversityService(UniversityDbContext context)
        {
            _context = context;
        }

        public List<Unversity> GetUniversities()
        {
            return _context.Universities.ToList();
        }
    }
}
