using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentApp.Models;

namespace StudentApp.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentContext _context;

        public StudentsController(StudentContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            return View(await _context.Students.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.Include(s => s.Courses)
                .FirstOrDefaultAsync(m => m.StudentID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentID,FullName")] Student student)
        {
            if (ModelState.IsValid)
            {
                student.StudentID = Guid.NewGuid();
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.Include(s => s.Courses).
                FirstOrDefaultAsync(s => s.StudentID == id);
            if (student == null)
            {
                return NotFound();
            }
            PopulateCourseList(student);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, string[] selectedCourses)
        {
            if (id == null)
            {
                return NotFound();
            }
            var studentToUpdate = await _context.Students
                .Include(s => s.Courses)
                .FirstOrDefaultAsync(s => s.StudentID == id);

            if (await TryUpdateModelAsync<Student>(
                studentToUpdate, "", s => s.FullName))
            {
                UpdateCourseList(studentToUpdate, selectedCourses);
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            UpdateCourseList(studentToUpdate, selectedCourses);
            PopulateCourseList(studentToUpdate);
            return View(studentToUpdate);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.StudentID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(Guid id)
        {
            return _context.Students.Any(e => e.StudentID == id);
        }
        
        private void PopulateCourseList(Student student)
        {
            var courses = _context.Courses;
            var studentCourse = new HashSet<Guid>(student.Courses.Select(c => c.CourseId));
            var viewModel = new List<AssignedCourse>();
            foreach (var course in courses)
            {
                viewModel.Add(new AssignedCourse
                {
                    CourseId = course.CourseId,
                    Title = course.CourseName,
                    Assigned = studentCourse.Contains(course.CourseId)
                });
            }
            ViewData["Courses"] = viewModel;
        }

        private void UpdateCourseList(Student studentToUpdate, string[] selectedCourses)
        {
            if (selectedCourses == null)
            {
                studentToUpdate.Courses = new List<Course>();
                return;
            }

            var selectedCoursesHS = new HashSet<string>(selectedCourses);
            var studentCourse = new HashSet<Guid>
                (studentToUpdate.Courses.Select(c => c.CourseId));

            foreach (var course in _context.Courses)
            {
                if (selectedCoursesHS.Contains(course.CourseId.ToString()))
                {
                    if (!studentCourse.Contains(course.CourseId))
                    {
                        studentToUpdate.Courses.Add(course);
                    }
                }

                else
                {
                    if(studentCourse.Contains(course.CourseId))
                    {
                        var courseToRemove = studentToUpdate.Courses.FirstOrDefault(x => x.CourseId == course.CourseId);
                        _context.Remove(courseToRemove);
                    }
                }
            }
        }
    }
}
