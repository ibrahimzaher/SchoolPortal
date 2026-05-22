namespace SchoolPortal.GradesApp.ViewModles;

public class GradeIndexViewModel
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public string StudentName { get; set; } = "Unknown Student";
    public string CourseName { get; set; } = string.Empty;
    public decimal Score { get; set; }
    public DateTime Date { get; set; }
}
