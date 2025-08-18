namespace ClassAct.Data.Models
{
    // Join entity for many-to-many relationship between Student and Course
    public class StudentCourse
    {
        public int StudentId { get; set; }
        public Student Student { get; set; } = default!;

        public int CourseId { get; set; }
        public Course Course { get; set; } = default!;
    }
}
