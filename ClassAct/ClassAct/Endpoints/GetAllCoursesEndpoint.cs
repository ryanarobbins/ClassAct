using FastEndpoints;
using ClassAct.Data;
using Microsoft.EntityFrameworkCore;
using ClassAct.Client.ViewModels; // Add this using for CourseViewModel

namespace ClassAct.Endpoints;

// Change return type to List<CourseViewModel>
public class GetAllCoursesEndpoint : EndpointWithoutRequest<List<CourseViewModel>>
{
    private readonly ClassActDbContext _db;

    public GetAllCoursesEndpoint(ClassActDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Get("/api/courses");
        AllowAnonymous();
    }

    public override async Task<List<CourseViewModel>> ExecuteAsync(CancellationToken ct)
    {
        // Map Course to CourseViewModel
        return await _db.Courses
            .Include(c => c.Instructor)
            .Select(c => new CourseViewModel
            {
                Id = c.Id,
                Title = c.Title,
                InstructorName = c.Instructor != null
                    ? c.Instructor.FirstName + " " + c.Instructor.LastName
                    : string.Empty
            })
            .ToListAsync(ct);
    }
}
