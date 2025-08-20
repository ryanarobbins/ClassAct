using ClassAct.Client.Services;
using ClassAct.Client.ViewModels;
using ClassAct.Data.Models;
using Microsoft.AspNetCore.Components;

namespace ClassAct.Client.Pages
{
    public partial class Students : ComponentBase
    {
        [Inject]
        public StudentService StudentService { get; set; } = default!;

        [Inject]
        public CourseService CourseService { get; set; } = default!;

        private List<Student> students = new();
        private List<CourseViewModel> courses = new();
        private Student newStudent = new();
        private bool isLoading = true;
        private string formMessage = string.Empty;
        private bool showHintCourse = false;
        private bool showHintAdd = false;
        private bool showProTipCourse = false;
        private bool showProTipAdd = false;

        protected Dictionary<int, int?> selectedCourses = new();
        protected Dictionary<int, (string message, bool isError)> assignMessages = new();

        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            students = await StudentService.GetAllStudentsAsync();
            courses = await CourseService.GetAllCoursesAsync();
            foreach (var student in students)
            {
                selectedCourses[student.Id] = null;
            }
            assignMessages = [];
            isLoading = false;
        }

        protected async Task HandleValidSubmit()
        {
            var result = await StudentService.AddStudentAsync(newStudent);
            if (result)
            {
                formMessage = "Student added successfully!";
                newStudent = new Student();
                await LoadDataAsync();
            }
            else
            {
                formMessage = "Failed to add student.";
            }
        }

        protected void ToggleHintAdd()
        {
            showHintAdd = !showHintAdd;
        }

        protected void ToggleHintCourse()
        {
            showHintCourse = !showHintCourse;
        }

        private void ToggleProTipAdd()
        {
            showProTipAdd = !showProTipAdd;
        }

        private void ToggleProTipCourse()
        {
            showProTipCourse = !showProTipCourse;
        }

        protected async Task AssignCourseToStudent(int studentId)
        {
            assignMessages.Remove(studentId);
            var courseId = selectedCourses[studentId];
            if (courseId is null || courseId == 0)
            {
                assignMessages[studentId] = ("Please select a course.", true);
                StateHasChanged();
                return;
            }

            var result = await CourseService.AssignStudentToCourseAsync(studentId, courses.Single(c => c.Id == courseId));
            
            await LoadDataAsync();

            if (result.IsSuccess)
            {
                assignMessages[studentId] = ("Student assigned to course successfully.", false);
            }
            else
            {
                assignMessages[studentId] = ($"Error: {result.ErrorMessage}", true);
            }
            StateHasChanged();
        }
    }
}
