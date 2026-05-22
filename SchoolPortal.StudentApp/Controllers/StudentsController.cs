using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolPortal.StudentApp.Data;
using SchoolPortal.StudentApp.Models;
using SchoolPortal.StudentApp.ViewModels;

namespace SchoolPortal.StudentApp.Controllers;

public class StudentsController : Controller
{
    private readonly StudentDbContext _context;

    public StudentsController(StudentDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var students = await _context.Students.ToListAsync();
        return View(students);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
        if (student == null) return NotFound();
        return View(student);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(StudentCreateViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var student = new Student
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            DateOfBirth = model.DateOfBirth,
            EnrollmentDate = model.EnrollmentDate
        };

        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var student = await _context.Students.FindAsync(id);
        if (student == null) return NotFound();

        var model = new StudentUpdateViewModel
        {
            Id = student.Id,
            FirstName = student.FirstName,
            LastName = student.LastName,
            Email = student.Email,
            DateOfBirth = student.DateOfBirth,
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, StudentUpdateViewModel model)
    {
        if (id != model.Id) return NotFound();
        if (!ModelState.IsValid) return View(model);

        var student = await _context.Students.FindAsync(id);
        if (student == null) return NotFound();

        student.FirstName = model.FirstName;
        student.LastName = model.LastName;
        student.Email = model.Email;
        student.DateOfBirth = model.DateOfBirth;

        _context.Students.Update(student);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
        if (student == null) return NotFound();
        return View(student);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student != null) _context.Students.Remove(student);

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}