using System.ComponentModel.DataAnnotations;

namespace SchoolPortal.StudentApp.ViewModels;

public class StudentCreateViewModel
{
    [Required(ErrorMessage = "First Name is required")]
    [StringLength(100, ErrorMessage = "First Name cannot exceed 100 characters")]
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last Name is required")]
    [StringLength(100, ErrorMessage = "Last Name cannot exceed 100 characters")]
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email Address is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address format")]
    [StringLength(30, ErrorMessage = "Email cannot exceed 30 characters")]
    [Display(Name = "Email Address")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Date of Birth is required")]
    [DataType(DataType.Date)]
    [Display(Name = "Date of Birth")]
    public DateOnly? DateOfBirth { get; set; }

    [Required(ErrorMessage = "Enrollment Date is required")]
    [DataType(DataType.DateTime)]
    [Display(Name = "Enrollment Date")]
    public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
}
