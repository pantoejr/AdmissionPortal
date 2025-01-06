using AdmissionPortal.Data;
using AdmissionPortal.Models;
using DevExpress.XtraRichEdit.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AdmissionPortal.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly AppDbContext _context;
        public DepartmentsController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var departments = await _context.Departments.Include(x => x.College).ToListAsync();
            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["CollegeID"] = new SelectList(_context.Colleges, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id", "CollegeID", "Name")] Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Add(department);
                department.IsActive = true;
                await _context.SaveChangesAsync();
                TempData["Message"] = "Record added successfully";
                TempData["Flag"] = "green";
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        public async Task<IActionResult> Details(int Id)
        {
            var department = await _context.Departments.Include(x => x.College).FirstOrDefaultAsync(x => x.Id == Id);
            if (department == null)
            {
                return NotFound();
            }
            ViewData["CollegeID"] = new SelectList(_context.Colleges, "Id", "Name", department.CollegeID);
            return View(department);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var department = await _context.Departments.Include(x => x.College)
                .FirstOrDefaultAsync(x => x.Id == Id);
            ViewData["CollegeID"] = new SelectList(_context.Colleges, "Id", "Name", department.CollegeID);
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Department model, int Id)
        {
            var department = await _context.Departments.FindAsync(Id);
            if (department == null)
            {
                return NotFound();
            }

            department.Name = model.Name;
            department.CollegeID = model.CollegeID;
            department.IsActive = model.IsActive;

            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Record updated successfully";
            TempData["Flag"] = "green";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var department = await _context.Departments.FindAsync(Id);
            if (department == null)
            {
                return NotFound();
            }
            department.IsActive = false;
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Record disabled successfully";
            TempData["Flag"] = "green";
            return RedirectToAction(nameof(Index));
        }
    }
}
