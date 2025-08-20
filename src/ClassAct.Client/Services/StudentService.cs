using ClassAct.Data.Models;
using System.Net.Http.Json;

namespace ClassAct.Client.Services
{
    public class StudentService
    {
        private readonly HttpClient _httpClient;

        // Inject HttpClient via constructor
        public StudentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Student>> GetStudentsAsync()
        {
            // Call the API endpoint instead of returning hardcoded data
            var students = await _httpClient.GetFromJsonAsync<List<Student>>("/api/students");
            return students ?? new List<Student>();
        }

        public async Task<bool> AddStudentAsync(Student student)
        {
            var response = await _httpClient.PostAsJsonAsync("api/students", student);
            return response.IsSuccessStatusCode;
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> AssignStudentToCourseAsync(int studentId, int courseId)
        {
            var req = new
            {
                StudentId = studentId,
                CourseId = courseId
            };

            var response = await _httpClient.PostAsJsonAsync("/api/students/assign-course", req);
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

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            var students = await _httpClient.GetFromJsonAsync<List<Student>>("api/students");
            return students ?? new List<Student>();
        }

        private class AssignStudentToCourseResponse
        {
            public bool Success { get; set; }
            public string? ErrorMessage { get; set; }
        }
    }
}
