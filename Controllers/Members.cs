using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using System;

[Route("/members")]
public class MembersController : Controller
{
    // DbSet<ApplicationUser> users;
    // private IApplicationUserRepository userRepo;
    private DB db;

    public MembersController(DB db ){
        this.db = db;
        // userRepo = users;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(db.Members.ToList()); // View(new Student) method takes an optional object as a "model", typically called a ViewModel
    }

    [Route("{id}")]
    [HttpGet]
    public IActionResult Details(int id)
    {
        var user = db.Members.Where(m => m.ItemID == id);
        return View(user);
    }
}