﻿using Microsoft.AspNetCore.Mvc;

namespace AdmissionPortal.Controllers
{
    public class DepartmentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
