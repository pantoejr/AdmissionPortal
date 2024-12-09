using Microsoft.AspNetCore.Mvc;

namespace AdmissionPortal.Controllers
{
    public class ApplicantDocumentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
