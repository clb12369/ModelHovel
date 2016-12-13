using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

// [Route("members/{username}/stash")]
// public class StashController : Controller
// {
//     // DbSet<ApplicationUser> users;
//     // private IApplicationUserRepository userRepo;
//     private DB db;

//     public StashController(DB db){
//         this.db = db;
//         // userRepo = users;
//     }

//     [HttpGet]
//     public IActionResult Index()
//     {
//         return View(db.Lists.ToList()); // View(new Student) method takes an optional object as a "model", typically called a ViewModel
//     }

//     [Route("{id}")]
//     [HttpGet]
//     public IActionResult Details(int id)
//     {
//         var user = db.Lists.FirstOrDefault(m => m.ItemID == id);
//         return View(user);
//     }

//     [RouteAttribute("new")]
//     [HttpGet]
//     public IActionResult CreateList()
//     {
//         return View();

//     }
//     // [RouteAttribute("new")]
//     //[HttpPost]
//     // public IActionResult CreateList([FromForm] StashList list)
//     // {
//     //     return View();

//     // }

//     [HttpGet]
//     public IActionResult AddStashItem()
//     {
//         return View();
//     }

//     [HttpPost]
//     public IActionResult AddStashItem([FromForm] StashItem item)
//     {
//         return View();

//     }
// }