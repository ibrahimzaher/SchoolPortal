using SchoolPortal.GradesApp.DTOs;
using System.Net;

namespace SchoolPortal.GradesApp.Services;

public class StudentsServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<StudentsServiceClient> _logger;
    public StudentsServiceClient(HttpClient httpClient, ILogger<StudentsServiceClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }
    public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<StudentDto>>("Students/GetAll");
            return response ?? [];
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching all students from Students Service.");
            return [];
        }
    }

    public async Task<StudentDto?> GetStudentByIdAsync(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"Students/GetById/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                _logger.LogWarning("Student with Id {StudentId} was not found.", id);
                return null;
            }

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<StudentDto>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching student {StudentId} from Students Service.", id);
            return null;
        }
    }
}
