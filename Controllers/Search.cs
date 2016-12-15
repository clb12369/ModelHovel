using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

[Route("/")]
[Authorize]
// [Route("/search")]
public class SearchController : Controller
{
    // DbSet<ApplicationUser> users;
    // private GoogleRootObject googleRootObject;
    // public SearchController(GoogleRootObject googleObj){
    //     googleRootObject = googleObj;
    // }

    // [HttpGet]
    // public IActionResult Index()
    // {
    //     return View(); // View(new Student) method takes an optional object as a "model", typically called a ViewModel
    // }

    // [AllowAnonymous]

    [HttpGet("/search/results")]
    public IActionResult Search(string searchTerm){
        return View("GoogleSearch");

    }

    [HttpPost("/search/results")]
    public async Task<IActionResult> SearchResults([FromForm] string searchTerm)
    {
        GoogleSearch search = new GoogleSearch();
        GoogleRootObject data = await search.CustomSearch(searchTerm);
        data.Log();
        return View(data);
    }
}
