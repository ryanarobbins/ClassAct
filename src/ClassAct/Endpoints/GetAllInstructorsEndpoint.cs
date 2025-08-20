using FastEndpoints;
using ClassAct.Data;
using ClassAct.Data.Models;
using Microsoft.EntityFrameworkCore;
using static ClassAct.Client.Pages.Instructors;
using ClassAct.Client.ViewModels;

namespace ClassAct.Endpoints;

public class GetAllInstructorsEndpoint : EndpointWithoutRequest<List<InstructorDto>>
{
    private readonly ClassActDbContext _db;

    public GetAllInstructorsEndpoint(ClassActDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Get("/api/instructors");
        AllowAnonymous();
    }

    public override async Task<List<InstructorDto>> ExecuteAsync(CancellationToken ct)
    {

        var instructors = await _db.Instructors
                .ToListAsync(ct);

        var instructorDtos = new List<InstructorDto>();

        foreach (var instructor in instructors)
        {
            var courses = await GetCoursesAsync(instructor, ct);
            instructorDtos.Add(new InstructorDto
            {
                FirstName = instructor.FirstName,
                LastName = instructor.LastName,
                IsGuest = instructor.IsGuest,
                Courses = courses
            });
        }

        return instructorDtos;
    }

    async Task<List<CourseViewModel>> GetCoursesAsync(Instructor instructor, CancellationToken ct)
    {
        return await _db.Courses
            .Where(c => c.InstructorId == instructor.Id)
            .Select(c => new CourseViewModel { Title = c.Title })
            .ToListAsync(ct);
    }
}
