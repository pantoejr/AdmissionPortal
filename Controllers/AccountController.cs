using AdmissionPortal.Data;
using AdmissionPortal.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdmissionPortal.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                SetTempDataMessage("Invalid input. Please try again.", "red");
                return View(model);
            }
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Username);
                if (user == null || !user.IsActive)
                {
                    SetTempDataMessage("Incorrect Username and Password", "red");
                    return View(model);
                }

                var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    SetTempDataMessage("Login successfull", "green");
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                SetTempDataMessage("An error occurred. Please try again later.", "red");
            }

            SetTempDataMessage("Incorrect Username and Password", "red");
            return View(model);
        }

        private void SetTempDataMessage(string message, string flag)
        {
            TempData["Message"] = message;
            TempData["Flag"] = flag;
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
