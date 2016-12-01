using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

[Authorize]
[Route("/account")]
public class AccountController : Controller
{
    private readonly IAuthService auth;
    public AccountController(IAuthService auth){
        this.auth = auth;
    }

    [HttpGet]
    public IActionResult Root()
    {
        // HttpContext.User
        return Ok();
    }

    [HttpGet("register")]
    [AllowAnonymous]
    public IActionResult Register()
    {
        // ViewData["Action"] = "Register";
        return View();
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromForm] RegisterView user)
    {
        // ViewData["Action"] = "Register";
        if(!ModelState.IsValid) return View(user);

        var errors = await auth.Register(user.FirstName, user.LastName, user.UserName, user.Email, user.Password, user.ModelingInterest);
        if((errors ?? new List<string>()).Count() == 0)
            return Redirect("/account");
        
        foreach(var e in errors) ModelState.AddModelError("", e);
        return View("RegisterOrLogin", user);
    }

    [HttpGet("login")]
    [AllowAnonymous]
    public IActionResult Login()
    {
        // ViewData["Action"] = "Login";
        return View();
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromForm] LoginView user)
    {
        ViewData["Action"] = "Login";

        if (!ModelState.IsValid) return View("RegisterOrLogin", user);

        string result = await auth.Login(user.Email, user.Password);
        if(result == null){
            return Redirect("/account");
        }

        ModelState.AddModelError("", result);
        return View("RegisterOrLogin", user);
    }

    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await auth.Logout();
        return Redirect("/");
    }
}

public class RegisterView
{
    [Required]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }
    [Required]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }
    [Required]
    [DisplayAttribute(Name = "Username")]
    public string UserName { get; set; }
    [Required]
    [DisplayAttribute(Name = "Email Address")]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [DisplayAttribute(Name = "Password")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required]
    [DisplayAttribute(Name = "Confirm Password")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The passwords do not match.")]
    public string ConfirmPassword { get; set; }
    [DisplayAttribute(Name = "Main Interests")]
    public string ModelingInterest { get; set; }
}

public class LoginView
{
    [Required]
    [DisplayAttribute(Name = "Email Address")]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [DisplayAttribute(Name = "Password")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

}