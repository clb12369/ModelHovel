using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;

[Route("/api/members")]
public class HomeAPIController : CRUDController<ApplicationUser> {
    public HomeAPIController(IRepository<ApplicationUser> r, DB db) : base(r, db){}



    // [HttpGet("search")]
    // public IActionResult Search([FromQuery]string term, int listId = -1){
    //     return Ok(r.Read(dbset => dbset.Where(card => 
    //         card.Title.ToLower().IndexOf(term.ToLower()) != -1
    //         || card.Text.ToLower().IndexOf(term.ToLower()) != -1
    //     )));
    // }
}

// [Route("/api/cardlist")]
// public class CardListController : CRUDController<CardList> {
//     public CardListController(IRepository<CardList> r) : base(r){}
// }

// [Route("/api/board")]
// public class BoardController : CRUDController<Board> {
//     public BoardController(IRepository<Board> r) : base(r){}
// }
