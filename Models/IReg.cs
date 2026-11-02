using StudentRegistrationAPI.Models;

namespace StudentRegistrationAPI.Services
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllStudents();

        Task<bool> AddStudent(Student student);
    }
}