using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using NuGet.Protocol;
using NuGet.Protocol.Plugins;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace EcomGalaxy.Controllers.Admin
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAccountService _accountService;
		private readonly IEmailSender _emailSender;

        public AccountController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, IAccountService accountService, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _accountService = accountService;
			_emailSender = emailSender;
		}

        [HttpGet]
        public IActionResult ChangePasswordForm()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePasswordVM)
        {
            if (ModelState.IsValid)
            {
                string UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                List<string> result = await _accountService.ChangePassword(changePasswordVM, UserId);
                if(result.Count != 0)
                {
                    foreach (var item in result)
                    {
                        ModelState.AddModelError("", item);
                    }
                }
            }
            return View("ChangePasswordForm", new ChangePasswordViewModel());
        }
        
        [HttpGet]
        public IActionResult ChangeUsernameForm()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeUsername(ChangeUsernameViewModel changeUsernameVM)
        {
            if (ModelState.IsValid)
            {
                string UserId = User.Claims.FirstOrDefault(c=>c.Type == ClaimTypes.NameIdentifier).Value;
                List<string> result = await _accountService.ChangeUsername(changeUsernameVM, UserId);
                if(result.Count == 0)
                {
                    return RedirectToAction("Profile", "Auth");
				}
                else if (result[0] == "UsernameExists")
                {
					ModelState.AddModelError("", "Username already exists.");
				}
				else
                {
					foreach (var item in result)
                    {
						ModelState.AddModelError("", item);
					}
				}
			}
			return View("ChangeUsernameForm", changeUsernameVM);
		}

        [HttpGet]
        public IActionResult ChangeFullNameForm()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeFullName(ChangeFullNameViewModel changeFullNameViewModel)
		{
			if (ModelState.IsValid)
			{
				string UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
				List<string> result = await _accountService.ChangeFullName(changeFullNameViewModel, UserId);
				if (result.Count == 0)
				{
					return RedirectToAction("Profile", "Auth");
				}
				else
				{
					foreach (var item in result)
					{
						ModelState.AddModelError("", item);
					}
				}
			}
			return View("ChangeFullNameForm", changeFullNameViewModel);
		}

        [HttpGet]
        public IActionResult ChangeEmailForm()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeEmail(ChangeEmailViewModel changeEmailVM)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                var existingUser = await _userManager.FindByEmailAsync(changeEmailVM.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError(string.Empty, "The email is already in use.");
                    return View("ChangeEmailForm", changeEmailVM);
                }

                var token = await _userManager.GenerateChangeEmailTokenAsync(user, changeEmailVM.Email);

                var param = new Dictionary<string, string>
                {
                    {"token", token },
                    {"email", changeEmailVM.Email },
                    {"userId", user.Id }
                };

                var callbackUrl = Url.Action("ConfirmEmailChange", "Account", param, protocol: Request.Scheme);

                await _emailSender.SendEmailAsync(changeEmailVM.Email, "Confirm your email change", $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                return RedirectToAction("Index", "Home");
            }

            return View("ChangeEmailForm", changeEmailVM);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmailChange(string token, string email, string userId)
        {
            if (token == null || email == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await _userManager.FindByIdAsync(userId);
           
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var result = await _userManager.ChangeEmailAsync(user, email, token);
            if (!result.Succeeded)
            {
                return View("Error");
            }

            await _userManager.SetUserNameAsync(user, user.UserName);

            return View("ConfirmEmailChange");
        }

        [HttpGet]
        public IActionResult ResetPasswordForm()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordVM)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(resetPasswordVM.Email);
                if(user == null)
                {
                    ModelState.AddModelError("", $"Can't find Email with address {resetPasswordVM.Email}");
					return View("ResetPasswordForm", resetPasswordVM);
				}

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var param = new Dictionary<string, string>
				{
					{"token", token },
					{"password", resetPasswordVM.NewPassword },
                    {"userId", user.Id }
				};

                var callbackUrl = Url.Action("ConfirmPasswordReset", "Account", param, protocol: Request.Scheme);

				await _emailSender.SendEmailAsync(resetPasswordVM.Email, "Reset Password", $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

				return RedirectToAction("LoginForm", "Account");
			}
            return View("ResetPasswordForm", resetPasswordVM);
		}

        [HttpGet]
        public async Task<IActionResult> ConfirmPasswordReset(string token, string password, string userId)
        {
			if (token == null || password == null)
			{
				return RedirectToAction("LoginForm", "Account");
			}

			var user = await _userManager.FindByIdAsync(userId);

			if (user == null)
			{
				return RedirectToAction("LoginForm", "Account");
			}

            await _userManager.ResetPasswordAsync(user, token, password);
            
            return RedirectToAction("LoginForm", "Account");
		}
	}
}
