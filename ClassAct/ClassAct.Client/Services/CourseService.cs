using System.Net.Http.Json;
using ClassAct.Client.ViewModels;

namespace ClassAct.Client.Services
{
    public class CourseService
    {
        private readonly HttpClient _http;

        public CourseService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<CourseViewModel>> GetAllCoursesAsync()
        {
            var result = await _http.GetFromJsonAsync<List<CourseViewModel>>("/api/courses");
            return result ?? new List<CourseViewModel>();
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> AssignStudentToCourseAsync(int studentId, CourseViewModel course)
        {
            var req = new
            {
                StudentId = studentId,
                Course = course
            };

            var response = await _http.PostAsJsonAsync("/api/students/assign-course", req);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<AssignStudentToCourseResponse>();
                if (result is not null && result.Success)
                    return (true, null);
                else
                    return (false, result?.ErrorMessage ?? "Unknown error");
            }
            return (false, "Server error");
        }

        private class AssignStudentToCourseResponse
        {
            public bool Success { get; set; }
            public string? ErrorMessage { get; set; }
        }
    }
}
