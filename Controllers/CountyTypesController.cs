using AdmissionPortal.Data;
using AdmissionPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdmissionPortal.Controllers
{
    public class CountyTypesController : Controller
    {
        private readonly AppDbContext _context;
        public CountyTypesController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var countyTypes = _context.CountyTypes.ToList();
            return View(countyTypes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CountyType model)
        {
            if (!String.IsNullOrEmpty(model.Name))
            {
                model.IsActive = true;
                await _context.CountyTypes.AddAsync(model);
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
            var countyType = await _context.CountyTypes.FirstOrDefaultAsync(x => x.Id == Id);
            return View(countyType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CountyType model, int Id)
        {
            var countyType = await _context.CountyTypes.FirstOrDefaultAsync(x => x.Id == Id);
            if (countyType == null)
            {
                TempData["Message"] = "Record not found";
                TempData["Flag"] = "red";
                return RedirectToAction(nameof(Index));
            }
            countyType.Name = model.Name;
            countyType.IsActive = model.IsActive;
            _context.CountyTypes.Update(countyType);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Record updated sucessfully";
            TempData["Flag"] = "green";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var countyType = await _context.CountyTypes.FirstOrDefaultAsync(x => x.Id == Id);
            countyType.IsActive = false;
            _context.CountyTypes.Update(countyType);
            await _context.SaveChangesAsync();
            TempData["Message"] = "CountyType disabled sucessfully";
            TempData["Flag"] = "green";
            return RedirectToAction("Index");
        }
    }
}
