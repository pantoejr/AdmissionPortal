using AdmissionPortal.Data;
using AdmissionPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdmissionPortal.Controllers
{
    public class TitleTypesController : Controller
    {
        private readonly AppDbContext _context;
        public TitleTypesController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var titleTypes = _context.TitleTypes.ToList();
            return View(titleTypes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TitleType model)
        {
            if (!String.IsNullOrEmpty(model.Name))
            {
                model.IsActive = true;
                await _context.TitleTypes.AddAsync(model);
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
            var titleType = await _context.TitleTypes.FirstOrDefaultAsync(x => x.Id == Id);
            return View(titleType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TitleType model, int Id)
        {
            var titleType = await _context.TitleTypes.FirstOrDefaultAsync(x => x.Id == Id);
            if (titleType == null)
            {
                TempData["Message"] = "Record not found";
                TempData["Flag"] = "red";
                return RedirectToAction(nameof(Index));
            }
            titleType.Name = model.Name;
            titleType.IsActive = model.IsActive;
            _context.TitleTypes.Update(titleType);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Record updated sucessfully";
            TempData["Flag"] = "green";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var titleType = await _context.TitleTypes.FirstOrDefaultAsync(x => x.Id == Id);
            titleType.IsActive = false;
            _context.TitleTypes.Update(titleType);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Title Type disabled sucessfully";
            TempData["Flag"] = "green";
            return RedirectToAction("Index");
        }
    }
}
