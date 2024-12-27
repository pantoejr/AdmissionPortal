using AdmissionPortal.Data;
using AdmissionPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdmissionPortal.Controllers
{
    public class ReligionTypesController : Controller
    {
        private readonly AppDbContext _context;
        public ReligionTypesController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var religionTypes = _context.ReligionTypes.ToList();
            return View(religionTypes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReligionType model)
        {
            if (!String.IsNullOrEmpty(model.Name))
            {
                model.IsActive = true;
                await _context.ReligionTypes.AddAsync(model);
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
            var religionType = await _context.ReligionTypes.FirstOrDefaultAsync(x => x.Id == Id);
            return View(religionType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ReligionType model, int Id)
        {
            var religionType = await _context.ReligionTypes.FirstOrDefaultAsync(x => x.Id == Id);
            if (religionType == null)
            {
                TempData["Message"] = "Record not found";
                TempData["Flag"] = "red";
                return RedirectToAction(nameof(Index));
            }
            religionType.Name = model.Name;
            religionType.IsActive = model.IsActive;
            _context.ReligionTypes.Update(religionType);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Record updated sucessfully";
            TempData["Flag"] = "green";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var religionType = await _context.ReligionTypes.FirstOrDefaultAsync(x => x.Id == Id);
            religionType.IsActive = false;
            _context.ReligionTypes.Update(religionType);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Religion Type disabled sucessfully";
            TempData["Flag"] = "green";
            return RedirectToAction("Index");
        }
    }
}
