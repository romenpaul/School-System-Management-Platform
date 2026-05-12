using SE_1st_projects.Models;
using SE_1st_projects.Service.Interface;
using SE_1st_projects.UnitOfWork.Interface;

namespace SE_1st_projects.Service.Application
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _uow;

        public DepartmentService(IUnitOfWork uow) => _uow = uow;

        public async Task<IEnumerable<DepartmentModel>> GetAllAsync() =>
            await _uow.Department.GetAllAsync();

        public async Task<DepartmentModel?> GetByIdAsync(int id) =>
            await _uow.Department.GetByIdAsync(id);



        public async Task<DepartmentModel> CreateAsync(DepartmentModel department)
        {
            await _uow.Department.AddAsync(department);
            await _uow.SaveChangesAsync();
            return department;
        }
        public async Task<bool> UpdateAsync(int id, DepartmentModel department)
        {
            var existingDepartment = await _uow.Department.GetByIdAsync(id);
            if (existingDepartment == null) return false;
            existingDepartment.DepartmentName = department.DepartmentName;

            _uow.Department.Update(existingDepartment);
            await _uow.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var department = await _uow.Department.GetByIdAsync(id);
            if (department == null) return false;
            _uow.Department.Delete(department);
            await _uow.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<DepartmentModel>> GetINIdAsync()
        {

            throw new NotImplementedException();
        }


    }
}
