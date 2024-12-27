using AdmissionPortal.Data;
using AdmissionPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdmissionPortal.Controllers
{
    public class CollegeTypesController : Controller
    {
        private readonly AppDbContext _context;
        public CollegeTypesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var collegeTypes = _context.CollegeTypes.ToList();
            return View(collegeTypes);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CollegeType model)
        {
            if (!String.IsNullOrEmpty(model.Name))
            {
                model.IsActive = true;
                await _context.CollegeTypes.AddAsync(model);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Record created successfully";
                TempData["Flag"] = "green";
                return RedirectToAction(nameof(Index));
            }
            TempData["Message"] = "Error creating record";
            TempData["Flag"] = "red";
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var collegeType = await _context.CollegeTypes.FirstOrDefaultAsync(x => x.Id == Id);
            return View(collegeType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CollegeType model, int Id)
        {
            var collegeType = await _context.CollegeTypes.FirstOrDefaultAsync(x => x.Id == Id);
            if (collegeType == null)
            {
                TempData["Message"] = "Record not found";
                TempData["Flag"] = "red";
                return RedirectToAction(nameof(Index));
            }
            collegeType.Name = model.Name;
            collegeType.IsActive = model.IsActive;
            _context.CollegeTypes.Update(collegeType);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Record updated sucessfully";
            TempData["Flag"] = "green";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var collegeType = await _context.CollegeTypes.FirstOrDefaultAsync(x=>x.Id == Id);
            collegeType.IsActive = false;
            _context.CollegeTypes.Update(collegeType);
            await _context.SaveChangesAsync();
            TempData["Message"] = "College Type disabled sucessfully";
            TempData["Flag"] = "green";
            return RedirectToAction("Index");
        }


    }
}
