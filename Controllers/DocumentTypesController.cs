using AdmissionPortal.Data;
using AdmissionPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdmissionPortal.Controllers
{
    public class DocumentTypesController : Controller
    {
        private readonly AppDbContext _context;
        public DocumentTypesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var documentTypes = _context.DocumentTypes.ToList();
            return View(documentTypes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DocumentType model)
        {
            if (!String.IsNullOrEmpty(model.Name))
            {
                model.IsActive = true;
                await _context.DocumentTypes.AddAsync(model);
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
            var documentType = await _context.DocumentTypes.FirstOrDefaultAsync(x => x.Id == Id);
            return View(documentType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DocumentType model, int Id)
        {
            var documentType = await _context.DocumentTypes.FirstOrDefaultAsync(x => x.Id == Id);
            if (documentType == null)
            {
                TempData["Message"] = "Record not found";
                TempData["Flag"] = "red";
                return RedirectToAction(nameof(Index));
            }
            documentType.Name = model.Name;
            documentType.IsActive = model.IsActive;
            _context.DocumentTypes.Update(documentType);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Record updated sucessfully";
            TempData["Flag"] = "green";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var documentType = await _context.DocumentTypes.FirstOrDefaultAsync(x => x.Id == Id);
            documentType.IsActive = false;
            _context.DocumentTypes.Update(documentType);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Document Type disabled sucessfully";
            TempData["Flag"] = "green";
            return RedirectToAction("Index");
        }

    }
}
