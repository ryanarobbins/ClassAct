using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;

namespace ClassAct.Client.Pages
{
    public class AddInstructorBase : ComponentBase
    {
        protected bool showProTip;
        protected bool showHint;

        [Inject]
        protected HttpClient Http { get; set; } = default!;

        protected InstructorInputModel Instructor { get; set; } = new();
        protected bool IsSubmitting { get; set; } = false;
        protected string? SuccessMessage { get; set; }
        protected string? ErrorMessage { get; set; }

        protected async Task HandleValidSubmit()
        {
            IsSubmitting = true;
            SuccessMessage = null;
            ErrorMessage = null;
            try
            {
                var response = await Http.PostAsJsonAsync("/api/instructors", Instructor);
                if (response.IsSuccessStatusCode)
                {
                    SuccessMessage = "Instructor added successfully!";
                    Instructor = new();
                }
                else
                {
                    ErrorMessage = "Failed to add instructor.";
                }
            }
            catch
            {
                ErrorMessage = "An error occurred while adding the instructor.";
            }
            finally
            {
                IsSubmitting = false;
            }
        }

        protected void ToggleProTip()
        {
            showProTip = !showProTip;
        }

        protected void ToggleHint()
        {
            showHint = !showHint;
        }

        public class InstructorInputModel
        {
            [Required]
            public string FirstName { get; set; } = string.Empty;
            [Required]
            public string LastName { get; set; } = string.Empty;
            public bool IsGuest { get; set; } = false;
        }
    }
}
