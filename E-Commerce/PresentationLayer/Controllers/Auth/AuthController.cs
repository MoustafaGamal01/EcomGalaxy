using EcomGalaxy.ApplicationLayer.Services.IServices;
using EcomGalaxy.Domain.Models;
using EcomGalaxy.DomainLayer.Models;
using EcomGalaxy.ViewModel.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcomGalaxy.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult RegisterForm()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(CustomerRegisterViewModel customerRegisterVM)
        {
            if (ModelState.IsValid)
            {
                List<string> answer = await _authService.Register(customerRegisterVM);

                if (answer.Count == 0)
                {
                    return RedirectToAction("LoginForm", "Auth");
                }
                else if (answer[0] == "EmailExists")
                {
                    ModelState.AddModelError("", "Email already exists.");
                    return View("RegisterForm", customerRegisterVM);
                }
                else
                {
                    foreach (var item in answer)
                    {
                        ModelState.AddModelError("", item);
                    }
                }
            }

            return View("RegisterForm", customerRegisterVM);
        }

        [HttpGet]
        public IActionResult LoginForm()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (ModelState.IsValid)
            {
                // Login now returns both the result and the user's role
                var (result, role) = await _authService.Login(loginVM);

                if (result == ResultEnum.Done)
                {
                    if (role == Roles.Admin)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (role == Roles.Seller)
                    {
                        return RedirectToAction("ProductsForSeller", "Product");
                    }
                    else // Customer
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            ModelState.AddModelError("", "Invalid Email or Password.");
            return View("LoginForm", loginVM);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            ResultEnum result = await _authService.Logout();

            if (result == ResultEnum.Done)
            {
                return RedirectToAction("LoginForm", "Auth");
            }

            ModelState.AddModelError("", "Can't logout.");
            return RedirectToAction("Index", "Home");
        }

    }
}