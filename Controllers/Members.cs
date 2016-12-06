using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

// [Route("/members")]
// public class MembersController : Controller
// {
//     private DB db;

//     public MembersController(DB db){
//         this.db = db;
//     }

//     [HttpGet]
//     public IActionResult Index()
//     {
//         var members = db.Members.ToList();
//         return View(members); // View(new Student) method takes an optional object as a "model", typically called a ViewModel
//     }

//     [Route("{username}")]
//     [HttpGet]
//     public IActionResult Details(string username)
//     {
//         var user = db.Members.FirstOrDefault(m => m.UserName == username);
//         user.Log();
//         // ViewData["ApplicationUser"] = user;
//         return View(user);
//     }
// }