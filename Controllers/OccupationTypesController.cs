using AdmissionPortal.Data;
using AdmissionPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdmissionPortal.Controllers
{
    public class OccupationTypesController : Controller
    {
        private readonly AppDbContext _context;
        public OccupationTypesController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var occupationTypes = _context.OccupationTypes.ToList();
            return View(occupationTypes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OccupationType model)
        {
            if (!String.IsNullOrEmpty(model.Name))
            {
                model.IsActive = true;
                await _context.OccupationTypes.AddAsync(model);
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
            var occupationType = await _context.OccupationTypes.FirstOrDefaultAsync(x => x.Id == Id);
            return View(occupationType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OccupationType model, int Id)
        {
            var occupationType = await _context.OccupationTypes.FirstOrDefaultAsync(x => x.Id == Id);
            if (occupationType == null)
            {
                TempData["Message"] = "Record not found";
                TempData["Flag"] = "red";
                return RedirectToAction(nameof(Index));
            }
            occupationType.Name = model.Name;
            occupationType.IsActive = model.IsActive;
            _context.OccupationTypes.Update(occupationType);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Record updated sucessfully";
            TempData["Flag"] = "green";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var occupationType = await _context.OccupationTypes.FirstOrDefaultAsync(x => x.Id == Id);
            occupationType.IsActive = false;
            _context.OccupationTypes.Update(occupationType);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Occupation Type disabled sucessfully";
            TempData["Flag"] = "green";
            return RedirectToAction("Index");
        }

    }
}
