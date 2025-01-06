using AdmissionPortal.Data;
using AdmissionPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AdmissionPortal.Controllers
{
    public class DegreesController : Controller
    {
        private readonly AppDbContext _context;
        public DegreesController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var degrees = await _context.Degrees.Include(x=>x.Department).ToListAsync();
            return View(degrees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id", "DepartmentID", "Name")] Degree degree)
        {
            if (ModelState.IsValid)
            {
                _context.Add(degree);
                degree.IsActive = true;
                await _context.SaveChangesAsync();
                TempData["Message"] = "Record added successfully";
                TempData["Flag"] = "green";
                return RedirectToAction(nameof(Index));
            }
            return View(degree);
        }

        public async Task<IActionResult> Details(int Id)
        {
            var degree = await _context.Degrees.Include(x => x.Department).FirstOrDefaultAsync(x => x.Id == Id);
            if (degree == null)
            {
                return NotFound();
            }
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "Id", "Name", degree.DepartmentID);
            return View(degree);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var degree = await _context.Degrees.Include(x => x.Department)
                .FirstOrDefaultAsync(x => x.Id == Id);
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "Id", "Name", degree.DepartmentID);
            return View(degree);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Degree model, int Id)
        {
            var degree = await _context.Degrees.FindAsync(Id);
            if (degree == null)
            {
                return NotFound();
            }

            degree.Name = model.Name;
            degree.DepartmentID = model.DepartmentID;
            degree.IsActive = model.IsActive;

            _context.Degrees.Update(degree);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Record updated successfully";
            TempData["Flag"] = "green";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var degree = await _context.Degrees.FindAsync(Id);
            if (degree == null)
            {
                return NotFound();
            }
            degree.IsActive = false;
            _context.Degrees.Update(degree);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Record disabled successfully";
            TempData["Flag"] = "green";
            return RedirectToAction(nameof(Index));
        }
    }
}
