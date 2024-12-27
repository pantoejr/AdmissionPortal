using AdmissionPortal.Data;
using AdmissionPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdmissionPortal.Controllers
{
    public class MaritalStatusTypesController : Controller
    {
        private readonly AppDbContext _context;
        public MaritalStatusTypesController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var maritalStatuses = _context.MaritalStatuses.ToList();
            return View(maritalStatuses);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MaritalStatusType model)
        {
            if (!String.IsNullOrEmpty(model.Name))
            {
                model.IsActive = true;
                await _context.MaritalStatuses.AddAsync(model);
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
            var maritalStatusType = await _context.MaritalStatuses.FirstOrDefaultAsync(x => x.Id == Id);
            return View(maritalStatusType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MaritalStatusType model, int Id)
        {
            var maritalStatusType = await _context.MaritalStatuses.FirstOrDefaultAsync(x => x.Id == Id);
            if (maritalStatusType == null)
            {
                TempData["Message"] = "Record not found";
                TempData["Flag"] = "red";
                return RedirectToAction(nameof(Index));
            }
            maritalStatusType.Name = model.Name;
            maritalStatusType.IsActive = model.IsActive;
            _context.MaritalStatuses.Update(maritalStatusType);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Record updated sucessfully";
            TempData["Flag"] = "green";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var maritalStatusType = await _context.MaritalStatuses.FirstOrDefaultAsync(x => x.Id == Id);
            maritalStatusType.IsActive = false;
            _context.MaritalStatuses.Update(maritalStatusType);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Marital Status disabled sucessfully";
            TempData["Flag"] = "green";
            return RedirectToAction("Index");
        }
    }
}
