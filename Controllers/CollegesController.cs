using Microsoft.AspNetCore.Mvc;

namespace AdmissionPortal.Controllers
{
    public class CollegesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
