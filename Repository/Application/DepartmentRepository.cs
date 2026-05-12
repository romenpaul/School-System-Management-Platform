using SE_1st_projects.Models;
using SE_1st_projects.Repository.Interface;

namespace SE_1st_projects.Repository.Application
{
    public class DepartmentRepository : BaseRepository<DepartmentModel>, IDepartmentRepository
    {
        public DepartmentRepository(MyDBContext context) : base(context)
        {
        }
    }
}


