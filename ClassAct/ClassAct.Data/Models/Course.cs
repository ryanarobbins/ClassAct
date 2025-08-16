namespace ClassAct.Data.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public int InstructorId { get; set; }
        public Instructor? Instructor { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
    }
}
