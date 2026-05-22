namespace SchoolPortal.GradesApp.Models;

public class Grade
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public string CourseName { get; set; } = null!;
    public decimal Score { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public string? Notes { get; set; }
}
