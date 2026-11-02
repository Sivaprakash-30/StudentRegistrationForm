using System.Data;
using Microsoft.Data.SqlClient;
using StudentRegistrationAPI.Models;

namespace StudentRegistrationAPI.Services
{
    public class StudentService : IStudentService
    {
        private readonly IConfiguration _configuration;

        public StudentService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string GetConnectionString()
        {
            return _configuration
                .GetConnectionString("DefaultConnection")!;
        }

        public async Task<bool> AddStudent(Student student)
        {
            using SqlConnection con =  new SqlConnection(GetConnectionString());
            using SqlCommand cmd = new SqlCommand("AddStudent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue( "@StudentName", student.StudentName);
            cmd.Parameters.AddWithValue( "@StudentID", student.StudentID);
            cmd.Parameters.AddWithValue( "@Department", student.Department);
            cmd.Parameters.AddWithValue( "@Gender", student.Gender);
            cmd.Parameters.AddWithValue("@Email", student.Email);
            cmd.Parameters.AddWithValue(  "@DOB", student.DOB);

            await con.OpenAsync();

            int rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        public async Task<List<Student>> GetAllStudents()
        {
            List<Student> students = new List<Student>();
            using SqlConnection con = new SqlConnection(GetConnectionString());
            using SqlCommand cmd = new SqlCommand("GetAllStudents", con);
            cmd.CommandType = CommandType.StoredProcedure;

            await con.OpenAsync();

            using SqlDataReader dr = await cmd.ExecuteReaderAsync();
            while (await dr.ReadAsync())
            {
                students.Add(new Student
                {
                    StudentId = Convert.ToInt32(dr["StudentId"]),
                    StudentName = dr["StudentName"].ToString(),
                    StudentID = dr["StudentID"].ToString(),
                    Department = dr["Department"].ToString(),
                    Gender = dr["Gender"].ToString(),
                    Email = dr["Email"].ToString(),
                    DOB = Convert.ToDateTime(dr["DOB"]) 
                });
            }
            return students;
        }
    }
}