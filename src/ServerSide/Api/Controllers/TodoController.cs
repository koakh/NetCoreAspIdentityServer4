using Api.Models.ToDo;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Api.Controllers
{
    //Take the template string in the controller’s route attribute, [Route("api/[controller]")]
    //Replace “[Controller]” with the name of the controller, which is the controller class name minus the “Controller” suffix.For this sample, the controller class name is TodoController and the root name is “todo”. ASP.NET Core routing is not case sensitive.
    //If the[HttpGet] attribute has a template string, append that to the path.This sample doesn’t use a template string.

    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        public ITodoRepository TodoItems { get; set; }

        public TodoController(ITodoRepository todoItems)
        {
            TodoItems = todoItems;
        }

        //GET Todo
        //
        //The GetAll method returns an IEnumerable.MVC automatically serializes the object to JSON and writes the JSON into the body of the response message. 
        //The response code for this method is 200, assuming there are no unhandled exceptions. (Unhandled exceptions are translated into 5xx errors.)
        //        
        //GET /api/todo
        [HttpGet]
        public IEnumerable<TodoItem> GetAll()
        {
            return TodoItems.GetAll();
        }

        //GET Todo
        //
        //"{id}" is a placeholder variable for the ID of the todo item.When GetById is invoked, it assigns the value of “{ id}” in the URL to the method’s id parameter.
        //Name = "GetTodo" creates a named route and allows you to link to this route in an HTTP Response.I’ll explain it with an example later.
        //Return values
        //If no item matches the requested ID, the method returns a 404 error.This is done by returning NotFound.
        //Otherwise, the method returns 200 with a JSON response body.This is done by returning an ObjectResult
        //
        //GET /api/<controller>/<id>
        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(string id)
        {
            var item = TodoItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        //Insert
        //
        //This is an HTTP POST method, indicated by the[HttpPost] attribute.The[FromBody] attribute tells MVC to get the value of the to-do item from the body of the HTTP request.
        //The CreatedAtRoute method returns a 201 response, which is the standard response for an HTTP POST method that creates a new resource on the server.
        //CreateAtRoute also adds a Location header to the response. The Location header specifies the URI of the newly created to-do item.
        //
        //POST /api/<controller>
        [HttpPost]
        public IActionResult Insert([FromBody] TodoItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            TodoItems.Add(item);
            return CreatedAtRoute("GetTodo", new { id = item.Key }, item);
        }

        //Update
        //
        //Update is similar to Create, but uses HTTP PUT. The response is 204 (No Content). 
        //According to the HTTP spec, a PUT request requires the client to send the entire updated entity, not just the deltas. 
        //To support partial updates, use HTTP PATCH.
        //
        //PUT /api/<controller>/<id>
        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] TodoItem item)
        {
            if (item == null || item.Key != id)
            {
                return BadRequest();
            }

            var todo = TodoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            TodoItems.Update(item);
            return new NoContentResult();
        }

        //Update with Patch
        //
        //To support partial updates, use HTTP PATCH.
        //This overload is similar to the previously shown Update, but uses HTTP PATCH. The response is 204 (No Content).
        //
        //PATCH /api/<controller>/<id>
        [HttpPatch("{id}")]
        public IActionResult Update([FromBody] TodoItem item, string id)
        {
            if (item == null)
            {
                return BadRequest();
            }

            var todo = TodoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            item.Key = todo.Key;

            TodoItems.Update(item);
            return new NoContentResult();
        }

        //Delete
        //DELETE /api/<controller>/<id>
        //The response is 204 (No Content).
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var todo = TodoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            TodoItems.Remove(id);
            return new NoContentResult();
        }
    }
}
