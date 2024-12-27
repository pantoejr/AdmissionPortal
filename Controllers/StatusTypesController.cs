using AdmissionPortal.Data;
using AdmissionPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdmissionPortal.Controllers
{
    public class StatusTypesController : Controller
    {
        private readonly AppDbContext _context;

        public StatusTypesController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var statusTypes = _context.StatusTypes.ToList();
            return View(statusTypes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StatusType model)
        {
            if (!String.IsNullOrEmpty(model.Name))
            {
                model.IsActive = true;
                await _context.StatusTypes.AddAsync(model);
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
            var statusType = await _context.StatusTypes.FirstOrDefaultAsync(x => x.Id == Id);
            return View(statusType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ReligionType model, int Id)
        {
            var statusType = await _context.StatusTypes.FirstOrDefaultAsync(x => x.Id == Id);
            if (statusType == null)
            {
                TempData["Message"] = "Record not found";
                TempData["Flag"] = "red";
                return RedirectToAction(nameof(Index));
            }
            statusType.Name = model.Name;
            statusType.IsActive = model.IsActive;
            _context.StatusTypes.Update(statusType);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Record updated sucessfully";
            TempData["Flag"] = "green";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var statusType = await _context.StatusTypes.FirstOrDefaultAsync(x => x.Id == Id);
            statusType.IsActive = false;
            _context.StatusTypes.Update(statusType);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Status Type disabled sucessfully";
            TempData["Flag"] = "green";
            return RedirectToAction("Index");
        }
    }
}
