using FastEndpoints;
using ClassAct.Data;
using ClassAct.Data.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace ClassAct.Endpoints;

public class AddInstructorRequest
{
    [Required]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    public string LastName { get; set; } = string.Empty;
    public bool IsGuest { get; set; }
}

[HttpPost("/api/instructors")]
[AllowAnonymous]
public class AddInstructorEndpoint : Endpoint<AddInstructorRequest, Instructor>
{
    private readonly ClassActDbContext _db;
    private readonly IConfiguration _config;

    public AddInstructorEndpoint(ClassActDbContext db, IConfiguration config)
    {
        _db = db;
        _config = config;
    }

    public override async Task HandleAsync(AddInstructorRequest req, CancellationToken ct)
    {
        var instructor = new Instructor
        {
            FirstName = req.FirstName,
            LastName = req.LastName,
            IsGuest = req.IsGuest
        };

        if (req.IsGuest)
        {
            var monthsConfig = _config["GuestInstructorMonths"];
            var months = int.Parse(monthsConfig!);
            instructor.GuestUntil = DateTimeOffset.UtcNow.AddMonths(months);
        }

        _db.Instructors.Add(instructor);
        await _db.SaveChangesAsync(ct);

        await Send.CreatedAtAsync($"/api/instructors/{instructor.Id}", instructor, cancellation: ct);
    }
}
