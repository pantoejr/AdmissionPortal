using AdmissionPortal.Data;
using AdmissionPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace AdmissionPortal.Controllers
{
    public class NationalityTypesController : Controller
    {
        private readonly AppDbContext _context;
        public NationalityTypesController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var nationalities = await _context.Nationalities.ToListAsync();
            return View(nationalities);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NationalityType model)
        {
            if (!String.IsNullOrEmpty(model.Name))
            {
                model.IsActive = true;
                await _context.Nationalities.AddAsync(model);
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
            var nationality = await _context.Nationalities.FirstOrDefaultAsync(x => x.Id == Id);
            return View(nationality);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(NationalityType model, int Id)
        {
            var nationality = await _context.Nationalities.FirstOrDefaultAsync(x => x.Id == Id);
            if (nationality == null)
            {
                TempData["Message"] = "Record not found";
                TempData["Flag"] = "red";
                return RedirectToAction(nameof(Index));
            }
            nationality.Name = model.Name;
            nationality.IsActive = model.IsActive;
            _context.Nationalities.Update(nationality);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Record updated sucessfully";
            TempData["Flag"] = "green";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var nationality = await _context.Nationalities.FirstOrDefaultAsync(x => x.Id == Id);
            nationality.IsActive = false;
            _context.Nationalities.Update(nationality);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Nationality Type disabled sucessfully";
            TempData["Flag"] = "green";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                TempData["Message"] = "Error reading file";
                TempData["Flag"] = "red";
                return RedirectToAction("Index");
            }
            var dataList = new List<NationalityType>();

            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var data = new NationalityType
                        {
                            Name = worksheet.Cells[row, 1].Text,
                            IsActive = true,
                        };
                        dataList.Add(data);
                    }
                }
            }
            await _context.Nationalities.AddRangeAsync(dataList);
            await _context.SaveChangesAsync();
            TempData["Message"] = "File uploaded and record inserted successfully";
            TempData["Flag"] = "green";
            return RedirectToAction("Index");
        }
    }
}
