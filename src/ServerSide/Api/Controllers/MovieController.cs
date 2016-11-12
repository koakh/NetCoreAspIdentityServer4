using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Api.Models.Movie;
using System;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class MovieController : Controller
    {
        private readonly IMovieRepository _repository;
        private readonly ILogger<MovieController> _logger;

        //DI Inject IMovieRepository and ILogger in Constructor
        public MovieController(IMovieRepository repository, ILogger<MovieController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        // GET: /api/<controller>/
        //The response is 200, assuming there are no unhandled exceptions. (Unhandled exceptions are translated into 5xx errors.)
        public IEnumerable<Movie> Index()
        {
            //_logger.LogInformation("Listing all items");
            return _repository.GetAll();
        }

        // GET: /api/<controller>/getfromquery
        //The response is 200, assuming there are no unhandled exceptions. (Unhandled exceptions are translated into 5xx errors.)
        [HttpGet("getfromquery", Name = "GetFromQuery")]
        public IEnumerable<Movie> GetFromQuery()
        {
            //_logger.LogInformation("Listing items from query");
            return _repository.SqlQuery("SELECT * FROM Movie WHERE id >= 1000;");
        }

        // GET: /api/<controller>/getfromqueryasync
        //The response is 200, assuming there are no unhandled exceptions. (Unhandled exceptions are translated into 5xx errors.)
        [HttpGet("getfromqueryasync", Name = "GetFromQueryAsync")]
        public async Task<IEnumerable<Movie>> GetFromQueryAsync()
        {
            //_logger.LogInformation("Listing items from query");
            return await _repository.SqlQueryAsync("SELECT * FROM Movie WHERE id >= 1000;");
        }

        //GET /api/<controller>/{id}
        //If no item matches the requested ID, the method returns a 404 error.This is done by returning NotFound.
        //Otherwise, the method returns 200 with a JSON response body.This is done by returning an ObjectResult
        [HttpGet("{id}", Name = "GetMovie")]
        public IActionResult GetById(Guid id)
        {
            var item = _repository.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }

        //POST /api/<controller>/movie
        //The response is 201, which is the standard response for an HTTP POST method that creates a new resource on the server.
        //CreateAtRoute also adds a Location header to the response. The Location header specifies the URI of the newly created to-do item.
        [HttpPost]
        public IActionResult Insert([FromBody] Movie item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _repository.Add(item);
            return CreatedAtRoute("GetMovie", new { id = item.ID }, item);
        }

        //PUT /api/<controller>/<id>
        //The response is 204 (No Content)
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] [Bind("ID,Title,ReleaseDate,Genre,Price,Rating")] Movie item)
        {
            var check = _repository.Find(id, true);
            if (item == null || check != null && check.ID != id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Update(item);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Exists(item.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return new NoContentResult();
        }

        //PATCH /api/<controller>/<id>
        //The response is 204 (No Content)
        [HttpPatch("{id}")]
        public IActionResult Update([FromBody] Movie item, Guid id)
        {
            var check = _repository.Find(id);
            if (item == null || check.ID != id)
            {
                return BadRequest();
            }

            item.ID = check.ID;

            _repository.Update(item);
            return new NoContentResult();
        }

        //DELETE /api/<controller>/<id>
        //The response is 204 (No Content)
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var item = _repository.Find(id);
            if (item == null || item.ID != id)
            {
                return BadRequest();
            }

            _repository.Remove(id);
            return new NoContentResult();
        }

        private bool Exists(Guid id)
        {
            return (_repository.Find(id) != null) ? true : false;
        }
    }
}
