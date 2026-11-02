using System.ComponentModel.DataAnnotations;

namespace StudentRegistrationAPI.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        [Required]
        [StringLength(50)]
        public string StudentName { get; set; } 
        [Required]
        [StringLength(20)]
        public string StudentID { get; set; } 
        [Required]
        public string Department { get; set; } 
        [Required]
        public string Gender { get; set; } 
        [Required]
        [EmailAddress]
        public string Email { get; set; } 
        [Required]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
    }
}