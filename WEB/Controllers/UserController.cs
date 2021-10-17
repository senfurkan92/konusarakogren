using DOMAIN.Entities;
using DTO.UserDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        /// <summary>
        /// signIn formunun sunulmasi
        /// validation icin jquery kullanilmistir
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInDto dto)
        {
            var proper = false;
            var user = await userManager.FindByEmailAsync(dto.Email);
            if (user != null)
            {
                if (!(await userManager.IsLockedOutAsync(user)))
                {
                    var result = await signInManager.PasswordSignInAsync(user, dto.Password, dto.RememberMe, false);
                    if (result.Succeeded)
                    {
                        proper = true;
                        await userManager.SetLockoutEndDateAsync(user, null);
                        await userManager.ResetAccessFailedCountAsync(user);
                    }
                    else
                    {
                        await userManager.AccessFailedAsync(user);
                        ModelState.AddModelError("", "Email or password is invalid");
                    }   
                }
                else
                {
                    ModelState.AddModelError("", $"Locked out until {await userManager.GetLockoutEndDateAsync(user)}");
                }
            }
            else
            {
                ModelState.AddModelError("", "Unregistered Email");
            }

            if (proper)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(dto);
            }
        }

        /// <summary>
        /// signUp formunun sunulmasi
        /// validation icin jquery kullanilmistir
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }   

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDto dto)
        {
            var proper = false;
            var user = new User()
            {
                Email = dto.Email,
                PhoneNumber = dto.Phone,
                UserName = dto.Email,             
            };
            var result = await userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded)
            {
                proper = true;
            }
            else
            {
                result.Errors.ToList().ForEach(error => ModelState.AddModelError("", error.Description));
            }

            if (proper)
            {
                return RedirectToAction("SignIn", "User");
            }
            else
            {
                return View(dto);
            }

        }
    }
}
