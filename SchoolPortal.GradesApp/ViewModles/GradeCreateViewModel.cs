using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SchoolPortal.GradesApp.ViewModles;

public class GradeCreateViewModel
{
    [Required(ErrorMessage = "Please select a student")]
    [Display(Name = "Student")]
    public int StudentId { get; set; }

    [Required(ErrorMessage = "Course name is required")]
    [StringLength(150)]
    [Display(Name = "Course Name")]
    public string CourseName { get; set; } = string.Empty;

    [Required]
    [Range(0, 100, ErrorMessage = "Score must be between 0 and 100")]
    public decimal Score { get; set; }

    [StringLength(500)]
    public string? Notes { get; set; }

    public IEnumerable<SelectListItem>? StudentsList { get; set; }
}