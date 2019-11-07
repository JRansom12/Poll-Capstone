using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using PollCapstone.Models;
using PollCapstone.Utility;

namespace PollCapstone.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public List<SelectListItem> UserRoles { get; private set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Display(Name="Super Admin")]
            public bool IsSuperAdmin { get; set; }

            [Required]
            public string Role { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            UserRoles = new List<SelectListItem>()
            {
                new SelectListItem { Value = "PollMaker", Text = "PollMaker"},
                new SelectListItem { Value = "PollTaker", Text = "PollTaker"},
                //new SelectListItem { Value = "UnassignedUser", Text = "UnassignedUser"},
            };
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email, Role = Input.Role, };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync(StaticDetails.PollMaker))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(StaticDetails.PollMaker));
                    }
                    if (!await _roleManager.RoleExistsAsync(StaticDetails.PollTaker))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(StaticDetails.PollTaker));
                    }

                    if (user.Role == "PollMaker")
                    {
                        await _userManager.AddToRoleAsync(user, StaticDetails.PollMaker);
                        return RedirectToAction("Create", "Pollmakers", new { id = user.Id });
                    }
                    if (user.Role == "PollTaker")
                    {
                        await _userManager.AddToRoleAsync(user, StaticDetails.PollTaker);
                        return RedirectToAction("Create", "PollTakers", new { id = user.Id });
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    await _signInManager.SignInAsync(user, isPersistent: false);

                }
            }

            // If we got this far, something failed, redisplay form
            return RedirectToAction("Index", "Home");
        }
    }
}
