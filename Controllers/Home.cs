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
        ApplicationUser user = 
            db
                .Members
                .Include(x => x.Stash)
                .FirstOrDefault(m => m.UserName == username);
        // user.Log();
        return View(user);
    }

    // [Route("members/{username}/stash")]
    // [HttpGet]
    // public IActionResult StashIndex()
    // {
    //     return View(); // View(new Student) method takes an optional object as a "model", typically called a ViewModel
    // }

    [HttpGet("members/{username}/stash/new")]
    public IActionResult AddToStash(string username) => View();

    [HttpPost("members/{username}/stash/new")]
    public IActionResult AddToStash([FromForm] StashItem item, string username) 
    {
        ApplicationUser user = db.Members.FirstOrDefault(m => m.UserName == username);
        // user.Log();
        // item.Log();
        user.Stash.Add(item);
        // item.Log();
        db.SaveChanges();
        return RedirectToAction("MemberDetails");
    }

    [HttpGet("members/{username}/stash/{id}/edit")]
    // public IActionResult EditStashItem(string username, int id){
    public IActionResult EditStashItem(string username, int id){
        // ApplicationUser user = db.Members.FirstOrDefault(m => m.UserName == username);
        // var itemToUpdate = db.Items.FirstOrDefault(i => i.ItemID == id);
        // var itemToUpdate = user.Stash.FirstOrDefault(i => i.ItemID == id);
        // StashItem item = user.Stash.FirstOrDefault(i => i.ItemID == id);
        StashItem item = db.Items.FirstOrDefault(i => i.ItemID == id);
        item.Log();
        return View(item);
    }

    [HttpPost("members/{username}/stash/{id}/edit")]
    public IActionResult EditStashItem([FromForm] StashItem item, int id, string username)
    {
        ApplicationUser user = db.Members.FirstOrDefault(m => m.UserName == username);
        StashItem itemToUpdate = db.Items.FirstOrDefault(i => i.ItemID == id);
        if (itemToUpdate != null){
            itemToUpdate.ItemName = item.ItemName;
            itemToUpdate.ItemNumber = item.ItemNumber;
            itemToUpdate.ItemType = item.ItemType;
            itemToUpdate.Manufacturer = item.Manufacturer;
            itemToUpdate.Scale = item.Scale;
            itemToUpdate.Comments = item.Comments;
            db.SaveChanges();
        }
        itemToUpdate.Log();
        return RedirectToAction("MemberDetails");
    }

    // [HttpPost]
    [HttpPost("members/{username}/stash/{id}/delete")]
    public IActionResult DeleteStashItem(string username, int id)
    {
        ApplicationUser user = db.Members.FirstOrDefault(m => m.UserName == username);
        var itemToDelete = db.Items.FirstOrDefault(i => i.ItemID == id);
        itemToDelete.Log();

        if (itemToDelete != null)
            db.Items.Remove(itemToDelete);
            db.SaveChanges();

        return Redirect($"/members/{username}");
    }

    [HttpGet("members/{username}/edit")]
    public IActionResult EditMember(string username){
        ApplicationUser user = db.Users.FirstOrDefault(u => u.UserName == username);
        
        if (user != null)
        {
            return View(user);
        }
        return NotFound();
    }

    
    [HttpPost("members/{username}/edit")]
    public IActionResult EditMember([FromForm] RegisterView user, string username){
        ApplicationUser userToUpdate = db.Members.FirstOrDefault(u => u.UserName == username);
        if (userToUpdate != null){
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            userToUpdate.UserName = user.UserName;
            userToUpdate.Email = user.Email;
            userToUpdate.ModelingInterest = user.ModelingInterest;
            db.SaveChanges();
        }
        userToUpdate.Log();
        return RedirectToAction("MemberDetails");
    }




    // [Route("members/{username}/stash/{id}")]
    // [HttpGet]
    // public IActionResult StashDetails(int id)
    // {
    //     var list = db.Lists.FirstOrDefault(m => m.ItemID == id);
    //     return View(list);
    // }

    // [Route("members/{username}/stash/new")]
    // [HttpGet]
    // public IActionResult CreateStash(){
    //     return View();
    // }

    // [HttpPost]
    // public IActionResult CreateStash([FromForm] StashList list){
    //     if (!ModelState.IsValid)
    //         return View(list);

    //     db.Lists.Add(list);
    //     db.SaveChanges();
    //     return Redirect("/members/{username}/stash");
    // }

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