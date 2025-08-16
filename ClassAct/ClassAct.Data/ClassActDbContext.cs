using Microsoft.EntityFrameworkCore;
using ClassAct.Data.Models;

namespace ClassAct.Data
{
    public class ClassActDbContext(DbContextOptions<ClassActDbContext> options) : DbContext(options)
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Instructor> Instructors => Set<Instructor>();
        public DbSet<StudentCourse> StudentCourses => Set<StudentCourse>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.StudentId);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(sc => sc.CourseId);

            modelBuilder.Entity<Instructor>().HasData(
                new Instructor { Id = 1, FirstName = "Alice", LastName = "Anderson" },
                new Instructor { Id = 2, FirstName = "Bob", LastName = "Brown" },
                new Instructor { Id = 3, FirstName = "Jan", LastName = "Smith" }
            );

            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Title = "Mathematics 101", InstructorId = 1 },
                new Course { Id = 2, Title = "Mathematics 101", InstructorId = 3 },
                new Course { Id = 3, Title = "History 101", InstructorId = 2 },
                new Course { Id = 4, Title = "History 102", InstructorId = 2 }
            );

            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, FirstName = "Charlie", LastName = "Clark", DateOfBirth = new DateTime(2005, 5, 1), Email = "charlie.clark@email.com" },
                new Student { Id = 2, FirstName = "Dana", LastName = "Davis", DateOfBirth = new DateTime(2006, 8, 15), Email = "dana.davis@email.com" }
            );

            modelBuilder.Entity<Instructor>()
                .Property(i => i.GuestUntil)
                .HasColumnType("datetimeoffset");
        }
    }
}
