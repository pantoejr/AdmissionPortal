using AdmissionPortal.Data;
using AdmissionPortal.Models;
using AdmissionPortal.Services;
using AdmissionPortal.ViewModels;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

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
                SetTempData("Token's invalid, please contact administrator", "red");
                return View(model);
            }

            var token = await _context.Tokens.FirstOrDefaultAsync(x => x.Value.Contains(model.Token));
            if (token == null || !token.IsActive)
            {
                SetTempData("Token's not valid, please contact administrator", "red");
                return RedirectToAction("Login");
            }

            if (token.HasEntered && DateTime.Now.Date >= token.ExpirationDate.Date)
            {
                token.IsActive = false;
                _context.Tokens.Update(token);
                await _context.SaveChangesAsync();
                SetTempData("Token is expired", "red");
                return RedirectToAction("Login");
            }

            if (token.HasEntered)
            {
                SetTempData("Token logged in successfully", "green");
                HttpContext.Session.SetString("Token", token.Value);
                return RedirectToAction("Biodata");
            }

            if (!_context.Applicants.Any(e => e.Token == token.Value))
            {
                var createdStatus = await _context.StatusTypes.FirstOrDefaultAsync(x => x.Name.Equals("Created"));
                var applicant = new Applicant
                {
                    Token = token.Value,
                    IsActive = true,
                    StatusTypeID = createdStatus.Id,
                };

                await _context.AddAsync(applicant);
            }

            token.HasEntered = true;
            token.DateEntered = DateTime.Now;
            _context.Tokens.Update(token);
            await _context.SaveChangesAsync();

            HttpContext.Session.SetString("Token", token.Value);
            return RedirectToAction("Biodata");
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login(TokenViewModel model)
        //{
        //    if (model.Token == null)
        //    {
        //        SetTempData("Token's invalid, please contact administrator", "red");
        //        return View(model);
        //    }

        //    var token = await _context.Tokens.FirstOrDefaultAsync(x => x.Value.Contains(model.Token));
        //    if (token == null || !token.IsActive || (token.HasEntered && DateTime.Now.Date >= token.ExpirationDate.Date))
        //    {
        //        SetTempData(token == null || !token.IsActive ? "Token's not valid, please contact administrator" : "Token is expired", "red");
        //        return RedirectToAction("Login");
        //    }

        //    if (token.HasEntered)
        //    {
        //        SetTempData("Token logged in successfully", "green");
        //        HttpContext.Session.SetString("Token", token.Value);
        //        return RedirectToAction("Biodata");
        //    }


        //    var createdStatus = await _context.StatusTypes.FirstOrDefaultAsync(x => x.Name.Equals("Created"));
        //    var existingApplicantToken = _context.Applicants.Any(e => e.Token == token.Value);

        //    if (!existingApplicantToken)
        //    {
        //        var applicant = new Applicant
        //        {
        //            Token = token.Value,
        //            StatusTypeID = createdStatus.Id,
        //        };
        //        token.HasEntered = true;
        //        token.DateEntered = DateTime.Now;
        //        _context.Tokens.Update(token);
        //        await _context.AddAsync(applicant);
        //        await _context.SaveChangesAsync();
        //        HttpContext.Session.SetString("Token", token.Value);
        //        return RedirectToAction("Biodata");
        //    }

        //    token.HasEntered = true;
        //    token.DateEntered = DateTime.Now;
        //    _context.Tokens.Update(token);
        //    await _context.SaveChangesAsync();
        //    HttpContext.Session.SetString("Token", token.Value);
        //    return RedirectToAction("Biodata");
        //}


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
                else if (applicant.StatusType.Name.Trim().Contains("Created"))
                {
                    TempData["Status"] = "Created";
                }
                return View(applicant);
            }

            return View();
        }

        [HttpPost]
        [AdmissionFilter]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Biodata(Applicant applicant)
        {
            return View(applicant);
        }

        private void SetViewData()
        {
            ViewData["TitleID"] = new SelectList(_context.TitleTypes, "Id", "Name");
            ViewData["GenderID"] = new SelectList(_context.GenderTypes, "Id", "Name");
            ViewData["NationalityID"] = new SelectList(_context.Nationalities, "Id", "Name");
            ViewData["CountryID"] = new SelectList(_context.CountryTypes, "Id", "Name");
            ViewData["CountyID"] = new SelectList(_context.CountyTypes, "Id", "Name");
            ViewData["ReligionID"] = new SelectList(_context.ReligionTypes, "Id", "Name");
            ViewData["MaritalStatusID"] = new SelectList(_context.MaritalStatuses, "Id", "Name");
            ViewData["OccupationID"] = new SelectList(_context.OccupationTypes, "Id", "Name");
            ViewData["RelationshipTypeID"] = new SelectList(_context.RelationshipTypes, "Id", "Name");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Token");
            return RedirectToAction("Login");
        }

    }
}
