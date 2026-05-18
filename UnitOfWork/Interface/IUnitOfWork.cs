using SE_1st_projects.Repository.Interface;

namespace SE_1st_projects.UnitOfWork.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IDepartmentRepository Department { get; }
        IStudentRepository Student { get; }
        IRoleRepository Role { get; }
        IUserRepository User { get; }

        // 🔥 ADD THIS
        ITeacherRepository Teacher { get; }

        Task<int> SaveChangesAsync();
    }
}