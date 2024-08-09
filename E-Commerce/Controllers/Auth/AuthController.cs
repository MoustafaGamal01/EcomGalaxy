using EcomGalaxy.ViewModel.Auth;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(CustomerRegisterViewModel customerRegisterVM)
        {
            if (ModelState.IsValid)
            {
                // Create Acc
                List<string> answer = await _authService.Register(customerRegisterVM);
                if (answer.Count == 0)
                {
                    return RedirectToAction("LoginForm", "Account");
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (ModelState.IsValid)
            {
                // Calling it 
                ResultEnum result = await _authService.Login(loginVM);
                if (result == ResultEnum.Done)
                {
                    string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
                    if (role == "Admin")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (role == "Seller")
                    {
                        return RedirectToAction("ProductsForSeller", "Product");
                    }
                    else // Cutomer
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ModelState.AddModelError("", "Invalid Email or Password.");
            return View("LoginForm", loginVM);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            ResultEnum result = await _authService.Logout();
            if (result == ResultEnum.Done)
            {
                LoginViewModel loginView = new LoginViewModel();
                return RedirectToAction("LoginForm", loginView);
            }
            ModelState.AddModelError("", "Can't logout.");
            return RedirectToAction("Index", "Home");
        }
    }
}
