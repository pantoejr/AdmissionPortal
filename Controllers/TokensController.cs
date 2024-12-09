using AdmissionPortal.Data;
using AdmissionPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace AdmissionPortal.Controllers
{
    [Authorize]
    public class TokensController : Controller
    {
        private readonly AppDbContext _context;
        public TokensController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var tokens = _context.Tokens.ToList();
            return View(tokens);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(int tokenNumber)
        {
            if (tokenNumber != 0)
            {
                for (int i = 0; i < tokenNumber; i++)
                {
                    var token = new Token
                    {
                        Value = this.GenerateRandomString(9),
                        HasEntered = false,
                        ExpirationDate = DateTime.Now.AddDays(30),
                        IsActive = false,
                    };
                    await _context.Tokens.AddAsync(token);
                    await _context.SaveChangesAsync();
                }
                TempData["Message"] = "Token created successfully";
                TempData["Flag"] = "green";
                return RedirectToAction(nameof(Index));
            }
            TempData["Message"] = "Error creating token";
            TempData["Flag"] = "red";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var token = _context.Tokens.FirstOrDefault(x => x.Id == Id);
            return View(token);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, Token model)
        {
            if (Id != model.Id)
            {
                TempData["Flag"] = "red";
                TempData["Message"] = "Error Updating token";
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Tokens.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TokenExists(model.Id))
                    {
                        TempData["Flag"] = "red";
                        TempData["Message"] = "Error Updating token";
                    }
                    else
                    {
                        TempData["Flag"] = "red";
                        TempData["Message"] = "Error Updating token";
                    }
                }
                TempData["Flag"] = "green";
                TempData["Message"] = "Token Updated Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Details(int Id)
        {
            var tokenExist = TokenExists(Id);
            if (tokenExist == true)
            {
                var token = await _context.Tokens.FirstOrDefaultAsync(x => x.Id == Id);
                return View(token);
            }
            TempData["Flag"] = "red";
            TempData["Message"] = "Error finding token";
            return RedirectToAction(nameof(Index));
        }

        private bool TokenExists(int Id)
        {
            return _context.Tokens.Any(e => e.Id == Id);
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var existingToken = await _context.Tokens.FirstOrDefaultAsync(x => x.Id == Id);
            if (existingToken != null)
            {
                _context.Tokens.Remove(existingToken);
                _context.SaveChanges();
                TempData["Flag"] = "green";
                TempData["Message"] = "Token removed successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["Flag"] = "red";
            TempData["Message"] = "Error removing token";
            return RedirectToAction(nameof(Index));
        }

        public string GenerateRandomString(int length)
        {
            var random = new Random();
            const string allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder sb = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                int randomIndex = random.Next(0, allowedChars.Length);
                sb.Append(allowedChars[randomIndex]);
            }

            return sb.ToString();
        }

        public IActionResult Print(int Id)
        {
            var existingToken = _context.Tokens.FirstOrDefaultAsync(x => x.Id == Id);
            return View();
        }

        public IActionResult Report()
        {
            return View();
        }
    }
}
