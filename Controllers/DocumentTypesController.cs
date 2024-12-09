using Microsoft.AspNetCore.Mvc;

namespace AdmissionPortal.Controllers
{
    public class DocumentTypesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
