using AdmissionPortal.Data;
using AdmissionPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdmissionPortal.Controllers
{
    public class RelationshipTypesController : Controller
    {
        private readonly AppDbContext _context;
        public RelationshipTypesController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var relationshipTypes = _context.RelationshipTypes.ToList();
            return View(relationshipTypes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RelationshipType model)
        {
            if (!String.IsNullOrEmpty(model.Name))
            {
                model.IsActive = true;
                await _context.RelationshipTypes.AddAsync(model);
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
            var relationshipType = await _context.RelationshipTypes.FirstOrDefaultAsync(x => x.Id == Id);
            return View(relationshipType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RelationshipType model, int Id)
        {
            var relationshipType = await _context.RelationshipTypes.FirstOrDefaultAsync(x => x.Id == Id);
            if (relationshipType == null)
            {
                TempData["Message"] = "Record not found";
                TempData["Flag"] = "red";
                return RedirectToAction(nameof(Index));
            }
            relationshipType.Name = model.Name;
            relationshipType.IsActive = model.IsActive;
            _context.RelationshipTypes.Update(relationshipType);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Record updated sucessfully";
            TempData["Flag"] = "green";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var relationshipType = await _context.RelationshipTypes.FirstOrDefaultAsync(x => x.Id == Id);
            relationshipType.IsActive = false;
            _context.RelationshipTypes.Update(relationshipType);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Relationship Type disabled sucessfully";
            TempData["Flag"] = "green";
            return RedirectToAction("Index");
        }
    }
}
