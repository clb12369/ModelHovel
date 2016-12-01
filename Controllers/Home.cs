using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using System;

[Route("/")]
public class HomeController : Controller
{
    private DbSet<ApplicationUser> users;

    public HomeController(DbSet<ApplicationUser> users){
        this.users = users;
    }

    [HttpGet]
    // public IActionResult Root()
    // {
    //     return View("Empty"); // View(new Student) method takes an optional object as a "model", typically called a ViewModel
    // }

    [Route("accounts")]
    [HttpGet]
    public IActionResult Index()
    {
        return View(users);
    }
}