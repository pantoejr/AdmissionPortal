using AdmissionPortal.Data;
using AdmissionPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AdmissionPortal.Controllers
{
    public class CollegesController : Controller
    {
        private readonly AppDbContext _context;
        public CollegesController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var colleges = await _context.Colleges
                .Include(x => x.CollegeType)
                .ToListAsync();

            return View(colleges);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["CollegeTypeID"] = new SelectList(_context.CollegeTypes, "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CollegeTypeID,Name,ShortName,PhoneNo,EmailAddress,Address")] College college)
        {
            if (ModelState.IsValid)
            {
                _context.Add(college);
                college.IsActive = true;
                await _context.SaveChangesAsync();
                TempData["Message"] = "Record added successfully";
                TempData["Flag"] = "green";
                return RedirectToAction(nameof(Index));
            }
            return View(college);
        }
        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var college = await _context.Colleges.Include(x => x.CollegeType).FirstOrDefaultAsync(x => x.Id == Id);
            if (college == null)
            {
                return NotFound();
            }
            ViewData["CollegeTypeID"] = new SelectList(_context.CollegeTypes, "Id", "Name", college.CollegeTypeID);
            return View(college);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(College model, int Id)
        {
            var college = await _context.Colleges.FindAsync(Id);
            if (college == null)
            {
                return NotFound();
            }

            college.Name = model.Name;
            college.ShortName = model.ShortName;
            college.Address = model.Address;
            college.EmailAddress = model.EmailAddress;
            college.CollegeTypeID = model.CollegeTypeID;
            college.PhoneNo = model.PhoneNo;
            college.IsActive = model.IsActive;

            _context.Colleges.Update(college);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Record updated successfully";
            TempData["Flag"] = "green";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int Id)
        {
            var college = await _context.Colleges.Include(x => x.CollegeType).FirstOrDefaultAsync(x => x.Id == Id);
            if (college == null)
            {
                return NotFound();
            }
            ViewData["CollegeTypeID"] = new SelectList(_context.CollegeTypes, "Id", "Name", college.CollegeTypeID);
            return View(college);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var college = await _context.Colleges.FindAsync(Id);
            if (college == null)
            {
                return NotFound();
            }
            college.IsActive = false;
            _context.Colleges.Update(college);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Record disabled successfully";
            TempData["Flag"] = "green";
            return RedirectToAction(nameof(Index));
        }

    }
}
