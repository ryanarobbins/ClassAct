using FastEndpoints;
using ClassAct.Data;
using ClassAct.Data.Models;
using Microsoft.EntityFrameworkCore;
using ClassAct.Client.ViewModels;

public class AssignStudentToCourseRequest
{
    public int StudentId { get; set; }
    public CourseViewModel Course { get; set; }
}

public class AssignStudentToCourseResponse
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
}

public class AssignStudentToCourseEndpoint : Endpoint<AssignStudentToCourseRequest, AssignStudentToCourseResponse>
{
    private readonly ClassActDbContext _db;

    public AssignStudentToCourseEndpoint(ClassActDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Post("/api/students/assign-course");
        AllowAnonymous();
    }

    public override async Task HandleAsync(AssignStudentToCourseRequest req, CancellationToken ct)
    {
        var student = await _db.Students.FindAsync(new object[] { req.StudentId }, ct);
        var course = await _db.Courses.SingleAsync(c => c.Title == req.Course.Title);

        if (student == null || course == null)
        {
            await Send.OkAsync(new AssignStudentToCourseResponse
            {
                Success = false,
                ErrorMessage = "Student or course not found."
            });
            return;
        }

        var alreadyAssigned = await _db.StudentCourses
            .AnyAsync(sc => sc.StudentId == req.StudentId && sc.CourseId == req.Course.Id, ct);

        if (alreadyAssigned)
        {
            await Send.OkAsync(new AssignStudentToCourseResponse
            {
                Success = false,
                ErrorMessage = "Student is already assigned to this course."
            });
            return;
        }

        _db.StudentCourses.Add(new StudentCourse
        {
            StudentId = req.StudentId,
            CourseId = req.Course.Id
        });

        await _db.SaveChangesAsync(ct);

        await Send.OkAsync(new AssignStudentToCourseResponse
        {
            Success = true
        });
    }
}
