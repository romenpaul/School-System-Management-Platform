using SE_1st_projects.Models;

namespace SE_1st_projects.Service.Interface
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentModel>> GetAllAsync();

        Task<DepartmentModel?> GetByIdAsync(int id);

        Task<DepartmentModel> CreateAsync(DepartmentModel department);

        Task<bool> UpdateAsync(int id, DepartmentModel department);

        Task<bool> DeleteAsync(int id);
    }
}