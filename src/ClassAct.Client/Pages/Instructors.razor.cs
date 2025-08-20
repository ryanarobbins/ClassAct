using ClassAct.Client.ViewModels;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace ClassAct.Client.Pages
{
    public partial class Instructors : ComponentBase
    {
        [Inject]
        private HttpClient Http { get; set; } = default!;

        protected List<InstructorDto>? instructors;

        protected override async Task OnInitializedAsync()
        {
            instructors = await Http.GetFromJsonAsync<List<InstructorDto>>("/api/instructors");
        }

        public class InstructorDto
        {
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public bool IsGuest { get; set; }
            public List<CourseViewModel>? Courses { get; set; }
        }
    }
}
