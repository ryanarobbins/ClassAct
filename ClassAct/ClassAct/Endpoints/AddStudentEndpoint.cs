using Microsoft.AspNetCore.Mvc;
using ClassAct.Data.Models;
using ClassAct.Data;

namespace ClassAct.Endpoints;

public static class AddStudentEndpoint
{
    public static void MapAddStudentEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapPost("/api/students", async ([FromBody] Student student, ClassActDbContext db) =>
        {
            db.Students.Add(student);
            await db.SaveChangesAsync();
            return Results.Created($"/api/students/{student.Id}", student);
        });
    }
}
