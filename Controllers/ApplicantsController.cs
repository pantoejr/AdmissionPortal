using AdmissionPortal.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdmissionPortal.Controllers
{

    [Authorize]
    public class ApplicantsController : Controller
    {
        private readonly string _uploadFolder;
        private readonly AppDbContext _context;

        public ApplicantsController()
        {
            _uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Applicant_Document");
        }

        public IActionResult Index()
        {
            return View();
        }

        //public async Task<IActionResult> Upload(IFormFile file)
        //{
        //    if (file != null && file.Length > 0)
        //    {
        //        // Ensure the uploads folder exists
        //        if (!Directory.Exists(_uploadFolder))
        //        {
        //            Directory.CreateDirectory(_uploadFolder);
        //        }

        //        // Create a unique file name
        //        var fileName = Path.GetFileName(file.FileName);
        //        var filePath = Path.Combine(_uploadFolder, fileName);

        //        // Save the file
        //        using (var stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            await file.CopyToAsync(stream);
        //        }

        //        // Save document details in the database
        //        var document = new ApplicantDocument
        //        {
        //            FileName = fileName,
        //            FilePath = filePath
        //        };

        //        _context.ApplicantDocument.Add(document);
        //        await _context.SaveChangesAsync();

        //        return RedirectToAction("Index");
        //    }

        //    return View();
        //}
    }
}
