using AdmissionPortal.Data;
using Microsoft.AspNetCore.Mvc;

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
    }
}
