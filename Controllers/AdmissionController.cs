using AdmissionPortal.Data;
using AdmissionPortal.Services;
using AdmissionPortal.ViewModels;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AdmissionPortal.Controllers
{

    public class AdmissionController : Controller
    {
        private readonly AppDbContext _context;
        public AdmissionController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(TokenViewModel model)
        {
            if (model.Token == null)
            {
                SetTempData("Invalid Token", "red");
                return View(model);
            }

            try
            {
                var token = await _context.Tokens.FirstOrDefaultAsync(x => x.Value.Contains(model.Token));

                if (token == null)
                {
                    SetTempData("Token's not valid, please contact administrator", "red");
                    return View(model);
                }

                if (!token.IsActive)
                {
                    SetTempData("Token's not valid, please contact administrator", "red");
                    return View(model);
                }

                if (token.HasEntered)
                {
                    if (DateTime.Now.Date >= token.ExpirationDate.Date)
                    {
                        token.IsActive = false;
                        _context.Tokens.Update(token);
                        await _context.SaveChangesAsync();
                        SetTempData("Token is expired", "red");
                        return RedirectToAction("Login");
                    }

                    SetTempData("Token logged in successfully", "green");
                    HttpContext.Session.SetString("Token", token.Value);
                    return RedirectToAction("Biodata");
                }

                token.HasEntered = true;
                token.DateEntered = DateTime.Now;
                _context.Tokens.Update(token);
                await _context.SaveChangesAsync();
                return RedirectToAction("Biodata");
            }
            catch (Exception ex)
            {
                SetTempData(ex.Message.Humanize(), "red");
                return View(model);
            }
        }

        private void SetTempData(string message, string flag)
        {
            TempData["Message"] = message;
            TempData["Flag"] = flag;
        }



        [HttpGet]
        [AdmissionFilter]
        public async Task<IActionResult> Biodata()
        {
            var session = HttpContext.Session.GetString("Token");
            var applicant = await _context.Applicants
                .Include(s => s.StatusType)
                .FirstOrDefaultAsync(x => x.Token.Trim().Contains(session));

            SetViewData();

            if (applicant != null)
            {
                if (applicant.StatusType.Name.Trim().Contains("Approved"))
                {
                    TempData["Status"] = "Approved";
                }
                return View(applicant);
            }

            return View();
        }

        private void SetViewData()
        {
            ViewData["TitleID"] = new SelectList(_context.TitleTypes, "Id", "Name");
            ViewData["GenderID"] = new SelectList(_context.GenderTypes, "Id", "Name");
            ViewData["NationalityID"] = new SelectList(_context.Nationalities, "Id", "Name");
            ViewData["CountryID"] = new SelectList(_context.CountryTypes, "Id", "Name");
            ViewData["ReligionID"] = new SelectList(_context.ReligionTypes, "Id", "Name");
            ViewData["MaritalStatusID"] = new SelectList(_context.MaritalStatuses, "Id", "Name");
            ViewData["OccupationID"] = new SelectList(_context.OccupationTypes, "Id", "Name");
            ViewData["RelationshipID"] = new SelectList(_context.RelationshipTypes, "Id", "Name");
        }

    }
}
