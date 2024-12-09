using Microsoft.AspNetCore.Mvc;

namespace AdmissionPortal.Controllers
{
    public class CollegeTypesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
