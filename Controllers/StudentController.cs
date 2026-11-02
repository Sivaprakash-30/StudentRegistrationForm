using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using StudentRegistrationAPI.Models;
using StudentRegistrationAPI.Services;

namespace StudentRegistrationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentService.GetAllStudents();
            return Ok(students);
        }
        [HttpPost]
        public async Task<IActionResult> AddStudent(Student student)
        {
            try
            {
                bool result = await _studentService.AddStudent(student);
                if (result)
                    return Ok(new { message = "Student registered successfully" });
                return BadRequest(new { message = "Registration failed" });
            }
            catch (SqlException existing) when (existing.Number == 2601 || existing.Number == 2627)
            {
                return Conflict(new
                {
                    message = "You are already a member kindly give another email"
                });
            }
        }
    }
}