using AdmissionPortal.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AdmissionPortal.Controllers
{
    public class CollegesController : Controller
    {
        private readonly AppDbContext _context;
        public CollegesController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var colleges = await _context.Colleges
                .Include(x => x.CollegeType)
                .ToListAsync();

            return View(colleges);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["CollegeTypeID"] = new SelectList(_context.CollegeTypes, "Id", "Name");
            return View();
        }
    }
}
