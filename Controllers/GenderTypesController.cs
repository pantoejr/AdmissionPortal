using AdmissionPortal.Data;
using AdmissionPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdmissionPortal.Controllers
{
    [Authorize]
    public class GenderTypesController : Controller
    {
        private readonly AppDbContext _context;
        public GenderTypesController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var genders = _context.GenderTypes.ToList();
            return View(genders);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GenderType model)
        {
            if (!String.IsNullOrEmpty(model.Name))
            {
                model.IsActive = true;
                await _context.GenderTypes.AddAsync(model);
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
            var genderType = await _context.GenderTypes.FirstOrDefaultAsync(x => x.Id == Id);
            return View(genderType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GenderType model, int Id)
        {
            var genderType = await _context.GenderTypes.FirstOrDefaultAsync(x => x.Id == Id);
            if (genderType == null)
            {
                TempData["Message"] = "Record not found";
                TempData["Flag"] = "red";
                return RedirectToAction(nameof(Index));
            }
            genderType.Name = model.Name;
            genderType.IsActive = model.IsActive;
            _context.GenderTypes.Update(genderType);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Record updated sucessfully";
            TempData["Flag"] = "green";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var genderType = await _context.GenderTypes.FirstOrDefaultAsync(x => x.Id == Id);
            genderType.IsActive = false;
            _context.GenderTypes.Update(genderType);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Gender Type disabled sucessfully";
            TempData["Flag"] = "green";
            return RedirectToAction("Index");
        }
    }
}
