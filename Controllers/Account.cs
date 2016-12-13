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
    private DB db;
    public AccountController(IAuthService auth, DB db){
        this.auth = auth;
        this.db = db;
    }

    // [HttpGet]
    // public IActionResult Root()
    // {
    //     // HttpContext.User
    //     return Ok();
    // }
    
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
        ViewData["Action"] = "Register";
        if(!ModelState.IsValid) return View(user);

        var errors = await auth.Register(user.FirstName, user.LastName, user.UserName, user.Email, user.Password, user.ModelingInterest);
        if((errors ?? new List<string>()).Count() == 0)
            return RedirectToAction("MemberIndex", "Home");
        
        foreach(var e in errors) ModelState.AddModelError("", e);
        return View(user);
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

        if (!ModelState.IsValid) return View("Login", user);

        string result = await auth.Login(user.UserName, user.Password);
        if(result == null){
            return RedirectToAction("MemberIndex", "Home");
            // return View("Register", user);
        }

        ModelState.AddModelError("", result);
        // return RedirectToAction("MemberIndex", "Home");
        //return View("MemberDetails", user);
        return View("Login", user);
    }

    // [HttpPost("/")]
    public async Task<IActionResult> Logout()
    {
        await auth.Logout();
        return RedirectToAction("Index", "Home");
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
    [Display(Name = "Username")]
    public string UserName { get; set; }
    [Required]
    [Display(Name = "Email Address")]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [Display(Name = "Password")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required]
    [Display(Name = "Confirm Password")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The passwords do not match.")]
    public string ConfirmPassword { get; set; }
    [Display(Name = "Main Interests")]
    public string ModelingInterest { get; set; }

    public string ItemID { get; set; }
}

public class LoginView
{
    [Required]
    [DisplayAttribute(Name = "User Name")]
    public string UserName { get; set; }
    [Required]
    [DisplayAttribute(Name = "Password")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

}