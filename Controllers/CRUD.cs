using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;

/*

RESTful takes these endpoints for each resource

API                     Description             Request body            Response body
--------------------------------------------------------------------------------------------
GET /api/todo           Get all to-do items     None                    Array of to-do items
GET /api/todo/{id}      Get an item by ID       None                    To-do item
POST /api/todo          Add a new item          To-do item              To-do item
PUT /api/todo/{id}      Update an existing item To-do item (full)       None
PATCH /api/todo/{id}    Update an existing item To-do item (partial)    None
DELETE /api/todo/{id}   Delete an item.         None                    None

*/

// Removed HasId inheritance
public abstract class CRUDController<T> : Controller where T: class, HasId
{
    protected IRepository<T> r;
    protected DB db;
    public CRUDController(IRepository<T> r, DB db){
        this.r = r;
        this.db = db;
    }

    [HttpPost]
    public IActionResult C([FromBody] T item) {
        if(!ModelState.IsValid)
            return BadRequest(ModelState.ToErrorObject());

        return Ok(r.Create(item));
    }

    [HttpGet]
    public IActionResult R() => Ok(r.Read());

    [HttpGet("{id}")]
    public IActionResult R(int id) {
        var item = r.Read(id);
        if(item == null)
            return NotFound();

        return Ok(item);
    }

    [HttpPut("{id}")]
    public IActionResult U(int id, [FromBody] T item){
        if(item.ItemID != id || !ModelState.IsValid || !r.Update(item))
            return BadRequest();

        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult D(int id){
        T item = r.Delete(id);
        if(item == null)
            return NotFound();

        return Ok(item);
    }
}
