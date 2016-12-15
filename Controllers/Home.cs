using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

[Authorize]
[Route("/")]
public class HomeController : Controller
{
    private DB db;
    public HomeController(DB db){
        this.db = db;
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpGet("/about")]
    public IActionResult About() => View();

    [AllowAnonymous]
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
        // item.Log();
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
        // itemToUpdate.Log();
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
            return View(new EditMemberView {
                
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                ModelingInterest = user.ModelingInterest

                ///.!--.!--.
            });
        }
        return NotFound();
    }

    
    [HttpPost("members/{username}/edit")]
    public IActionResult EditMember([FromForm] EditMemberView user, string username){
        var userToUpdate = db.Members.FirstOrDefault(u => u.UserName == username);
        if (userToUpdate != null){
            db.Members.Remove(userToUpdate);
            userToUpdate.Id = user.Id;
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            userToUpdate.UserName = user.UserName;
            userToUpdate.Email = user.Email;
            userToUpdate.ModelingInterest = user.ModelingInterest;
            db.SaveChanges();
        }
        // userToUpdate.Log();
        return RedirectToAction("MemberDetails");
    }

    [HttpPost("/members/{username}/delete")]
    public IActionResult DeleteMember(string username){
        ApplicationUser user = db.Users.FirstOrDefault(u => u.UserName == username);
        if (user != null){
            db.Users.Remove(user);
            db.SaveChanges();
        }
        return Redirect($"/members");
    }
}

public class EditMemberView
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

    public string Id { get; }

    public string ItemID { get; set; }

    // public static EditMemberView FromUser(ApplicationUser u){
    //     return new EditMemberView{};
    // }

    // public static ApplicationUser FromVM(EditMemberView v){
    //     return new ApplicationUser{};
    // }
}