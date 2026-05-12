using SE_1st_projects.Models;
using SE_1st_projects.Service.Interface;
using SE_1st_projects.UnitOfWork.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SE_1st_projects.Service.Application
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _uow;

        public StudentService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<StudentModel>> GetAllAsync()
        {
            return await _uow.Student.GetAllAsync();
        }

        public async Task<StudentModel?> GetByIdAsync(int id)
        {
            return await _uow.Student.GetByIdAsync(id);
        }

        public async Task<StudentModel> CreateAsync(StudentModel student)
        {
            await _uow.Student.AddAsync(student);
            await _uow.SaveChangesAsync();
            return student;
        }

        public async Task<bool> UpdateAsync(int id, StudentModel student)
        {
            var existing = await _uow.Student.GetByIdAsync(id);
            if (existing == null) return false;

            existing.Name = student.Name;
            existing.Age = student.Age;
            existing.Email = student.Email;
            existing.Address = student.Address;
            existing.DepartmentId = student.DepartmentId;

            _uow.Student.Update(existing);
            await _uow.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var student = await _uow.Student.GetByIdAsync(id);
            if (student == null) return false;

            _uow.Student.Delete(student);
            await _uow.SaveChangesAsync();
            return true;
        }

        public async Task<(IEnumerable<StudentModel> Students, int TotalCount, int TotalPages)> GetPagedAsync(int page, int pageSize)
        {
            var result = await _uow.Student.GetPagedAsync(page, pageSize);
            int totalPages = (int)Math.Ceiling((double)result.TotalCount / pageSize);
            return (result.Students, result.TotalCount, totalPages);
        }
    }
}
