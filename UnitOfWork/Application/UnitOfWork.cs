using SE_1st_projects.Repository.Interface;
using SE_1st_projects.UnitOfWork.Interface;

namespace SE_1st_projects.UnitOfWork.Application
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyDBContext _Context;

        public UnitOfWork(MyDBContext context, IDepartmentRepository departmentRepository, IStudentRepository studentRepository, IRoleRepository roleRepository, IUserRepository userRepository)
        {
            _Context = context;
            Department = departmentRepository;
            Student = studentRepository;
            Role = roleRepository;
            User = userRepository;
        }

        public IDepartmentRepository Department { get; }
        public IStudentRepository Student { get; }
        public IRoleRepository Role { get; }
        public IUserRepository User { get; }

        public async Task<int> SaveChangesAsync() =>
            await _Context.SaveChangesAsync();

        public void Dispose() =>
            _Context.Dispose();
    }
}
