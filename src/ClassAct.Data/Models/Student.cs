namespace ClassAct.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        [NotMapped]
        public List<string> CourseNames { get; set; }

        // Navigation property for many-to-many relationship
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
    }
}
