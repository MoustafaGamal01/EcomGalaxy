using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EcomGalaxy.Controllers
{
    public class UserController : Controller
    {
        private readonly List<string> _roles;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<ApplicationUser> userManager, IUserService userService,
            RoleManager<IdentityRole> roleManager)
        {
            _roles = new List<string> { "Admin", "Seller" };
            _userManager = userManager;
            _userService = userService;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult AddUserForm()
        {
            ViewData["MyRoles"] = _roles;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser(UserRegisterViewModel userRegisterVM)
        {
            if (ModelState.IsValid)
            {
                List<string> answer = await _userService.AddUser(userRegisterVM);

                if (answer.Count == 0)
                {
                    RedirectToAction("", "");
                }
                else if (answer[0] == "EmailExists")
                {
                    ModelState.AddModelError("", "Email Is Already Taken");
                    return View("RegisterForm", userRegisterVM);
                }
                else
                {
                    foreach (var item in answer)
                    {
                        ModelState.AddModelError("", item);
                    }
                }
            }
            ViewData["MyRoles"] = _roles;
            return View("AddUserForm", userRegisterVM);
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            ProfileViewModel profileVM = new ProfileViewModel();

            profileVM.Name = user.Name;
            profileVM.UserName = user.UserName;
            profileVM.Email = user.Email;
            profileVM.Country = user.Country;
            profileVM.City = user.City;
            profileVM.Street = user.Street;
            profileVM.PostalCode = user.PostalCode;

            return View(profileVM);
        }

        [HttpGet]
        public async Task<IActionResult> ManageUsers()
        {
            var users = await _userManager.Users.ToListAsync();

            IEnumerable<ShowUserViewModel> usersVM = users.Select(u => new ShowUserViewModel
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Role = _userManager.GetRolesAsync(u).Result.FirstOrDefault()
            });

            return View(usersVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }


            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Count > 0)
            {
                var result = await _userManager.RemoveFromRolesAsync(user, roles);
                if (!result.Succeeded)
                {

                    ModelState.AddModelError("", "Error removing user from roles.");
                    return View("Error");
                }
            }

            var deleteResult = await _userManager.DeleteAsync(user);
            return RedirectToAction("GetUsersForm");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }
    }
}
