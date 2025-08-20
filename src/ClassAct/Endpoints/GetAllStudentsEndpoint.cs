using FastEndpoints;
using ClassAct.Data;
using ClassAct.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ClassAct.Endpoints;

public class GetAllStudentsEndpoint : EndpointWithoutRequest<List<Student>>
{
    private readonly ClassActDbContext _db;

    public GetAllStudentsEndpoint(ClassActDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Get("/api/students");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var students = await _db.Students
            .Select(s => new Student  
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                DateOfBirth = s.DateOfBirth,
                CourseNames = s.StudentCourses.Select(sc => sc.Course.Title).ToList(),
            })
            .ToListAsync(ct);

        await Send.OkAsync(students, ct);
    }
}
