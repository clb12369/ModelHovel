using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using System;

[Route("/")]
public class HomeController : Controller
{
    // DbSet<ApplicationUser> users;
    private DB db;

    public HomeController(DB db){
        this.db = db;
    }

    [HttpGet]
    // public IActionResult Index()
    // {
    //     return View(); // View(new Student) method takes an optional object as a "model", typically called a ViewModel
    // }

    // [Route("accounts")]
    [HttpGet]
    public IActionResult Index()
    {
        return View(db.Members.ToList());
    }
}