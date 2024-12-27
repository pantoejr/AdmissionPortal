using AdmissionPortal.Data;
using AdmissionPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace AdmissionPortal.Controllers
{
    public class CountryTypesController : Controller
    {
        private readonly AppDbContext _context;
        public CountryTypesController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var countryTypes = await _context.CountryTypes.ToListAsync();
            return View(countryTypes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CountryType model)
        {
            if (!String.IsNullOrEmpty(model.Name))
            {
                model.IsActive = true;
                await _context.CountryTypes.AddAsync(model);
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
            var countryType = await _context.CountryTypes.FirstOrDefaultAsync(x => x.Id == Id);
            return View(countryType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CountryType model, int Id)
        {
            var countryType = await _context.CountryTypes.FirstOrDefaultAsync(x => x.Id == Id);
            if (countryType == null)
            {
                TempData["Message"] = "Record not found";
                TempData["Flag"] = "red";
                return RedirectToAction(nameof(Index));
            }
            countryType.Name = model.Name;
            countryType.IsActive = model.IsActive;
            _context.CountryTypes.Update(countryType);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Record updated sucessfully";
            TempData["Flag"] = "green";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var countryType = await _context.CountryTypes.FirstOrDefaultAsync(x => x.Id == Id);
            countryType.IsActive = false;
            _context.CountryTypes.Update(countryType);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Country Type disabled sucessfully";
            TempData["Flag"] = "green";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UploadExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                TempData["Message"] = "Error reading file";
                TempData["Flag"] = "red";
                return RedirectToAction("Index");
            }
            var dataList = new List<CountryType>();

            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var data = new CountryType
                        {
                            Name = worksheet.Cells[row, 1].Text,
                            IsActive = true,
                        };
                        dataList.Add(data);
                    }
                }
            }
            await _context.CountryTypes.AddRangeAsync(dataList);
            await _context.SaveChangesAsync();
            TempData["Message"] = "File uploaded and record inserted successfully";
            TempData["Flag"] = "green";
            return RedirectToAction("Index");
        }
    }
}
