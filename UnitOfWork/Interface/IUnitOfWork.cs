using SE_1st_projects.Repository.Interface;

namespace SE_1st_projects.UnitOfWork.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IDepartmentRepository Department { get; }
        IStudentRepository Student { get; }
        IRoleRepository Role { get; }
        IUserRepository User { get; }
        Task<int> SaveChangesAsync();
    }

}
