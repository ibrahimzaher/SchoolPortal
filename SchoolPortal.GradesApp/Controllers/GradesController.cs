using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolPortal.GradesApp.Data;
using SchoolPortal.GradesApp.Models;
using SchoolPortal.GradesApp.Services;
using SchoolPortal.GradesApp.ViewModles;

namespace SchoolPortal.GradesApp.Controllers;

public class GradesController : Controller
{
    private readonly GradeDbContext _context;
    private readonly StudentsServiceClient _studentsService;
    private readonly ILogger<GradesController> _logger;

    public GradesController(GradeDbContext context, StudentsServiceClient studentsService, ILogger<GradesController> logger)
    {
        _context = context;
        _studentsService = studentsService;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var grades = await _context.Grades.OrderByDescending(g => g.Date).ToListAsync();

        var students = await _studentsService.GetAllStudentsAsync();
        var studentsDict = students.ToDictionary(s => s.Id, s => $"{s.FirstName} {s.LastName}");

        var viewModel = grades.Select(g => new GradeIndexViewModel
        {
            Id = g.Id,
            StudentId = g.StudentId,
            CourseName = g.CourseName,
            Score = g.Score,
            Date = g.Date,
            StudentName = studentsDict.TryGetValue(g.StudentId, out var name) ? name : "Unknown Student (Service Unavailable)"
        }).ToList();

        return View(viewModel);
    }

    public async Task<IActionResult> Create()
    {
        var students = await _studentsService.GetAllStudentsAsync();

        var model = new GradeCreateViewModel
        {
            StudentsList = students.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = $"{s.FirstName} {s.LastName}"
            })
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(GradeCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var students = await _studentsService.GetAllStudentsAsync();
            model.StudentsList = students.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = $"{s.FirstName} {s.LastName}"
            });
            return View(model);
        }

        var grade = new Grade
        {
            StudentId = model.StudentId,
            CourseName = model.CourseName,
            Score = model.Score,
            Notes = model.Notes,
            Date = DateTime.UtcNow
        };

        _context.Grades.Add(grade);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int id)
    {
        var grade = await _context.Grades.FindAsync(id);
        if (grade == null) return NotFound();

        var student = await _studentsService.GetStudentByIdAsync(grade.StudentId);

        ViewBag.StudentName = student != null ? $"{student.FirstName} {student.LastName}" : "Unknown Student";
        return View(grade);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var grade = await _context.Grades.FindAsync(id);
        if (grade == null) return NotFound();

        var student = await _studentsService.GetStudentByIdAsync(grade.StudentId);
        ViewBag.StudentName = student != null ? $"{student.FirstName} {student.LastName}" : "Unknown Student";

        return View(grade);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var grade = await _context.Grades.FindAsync(id);
        if (grade != null)
        {
            _context.Grades.Remove(grade);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}