using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using ClassAct.Client.ViewModels; // Add this

namespace ClassAct.Client.Pages
{
    public partial class Courses : ComponentBase
    {
        [Inject]
        private HttpClient Http { get; set; } = default!;

        // Use CourseViewModel instead of CourseDto
        protected List<CourseViewModel>? courses;

        protected override async Task OnInitializedAsync()
        {
            courses = await Http.GetFromJsonAsync<List<CourseViewModel>>("/api/courses");
        }

        // Remove CourseDto and InstructorDto classes
    }
}
