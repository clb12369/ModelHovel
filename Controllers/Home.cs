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

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [Route("members")]
    [HttpGet]
    public IActionResult MemberIndex()
    {
        return View(db.Members.ToList());
    }


    [Route("members/{username}")]
    [HttpGet]
    public IActionResult MemberDetails(string username)
    {
        var user = db.Members.FirstOrDefault(m => m.UserName == username);
        user.Log();
        // ViewData["ApplicationUser"] = user;
        return View(user);
    }

    [Route("members/{username}/stash")]
    [HttpGet]
    public IActionResult StashIndex()
    {
        return View(db.Lists.ToList()); // View(new Student) method takes an optional object as a "model", typically called a ViewModel
    }

    // [Route("members/{username}/stash/{id}")]
    // [HttpGet]
    // public IActionResult StashDetails(int id)
    // {
    //     var list = db.Lists.FirstOrDefault(m => m.ItemID == id);
    //     return View(list);
    // }

    [Route("members/{username}/stash/new")]
    [HttpGet]
    public IActionResult CreateStash(){
        return View();
    }

    [HttpPost]
    public IActionResult CreateStash([FromForm] StashList list){
        if (!ModelState.IsValid)
            return View(list);

        db.Lists.Add(list);
        db.SaveChanges();
        return Redirect("/members/{username}/stash");
    }

    // [Route("/members/{username}/stash/{id}/new")]
    // [HttpGet]
    // public IActionResult AddToStash() {
    //     return View();
    // }

    // [Route("members/{username}/stash/{id}/new")]
    // [HttpPost]
    // public IActionResult AddToStash([FromForm] StashItem item, string username, int id){
    //     ApplicationUser user = db.Members.FirstOrDefault(m => m.UserName == username);
    //     StashList list = user.StashLists.FirstOrDefault(l => l.ItemID == id);
    //     TryValidateModel(item);

    //     if (ModelState.IsValid)
    //     {
    //         user.list.Add(item);
    //         db.SaveChanges();
    //     }
    //     return Redirect($"/members/{username}"); 
    // }





}