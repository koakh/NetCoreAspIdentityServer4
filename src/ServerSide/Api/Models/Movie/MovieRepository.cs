using Api.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Movie
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext _context;

        //DI Inject ApplicationDbContext
        public MovieRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Movie> GetAll()
        {
            var movie = _context.Movie;
            return movie;
        }

        public IEnumerable<Movie> SqlQuery(string query, params object[] parameters)
        {
            return _context.Movie.FromSql(query);
        }

        public async Task<IEnumerable<Movie>> SqlQueryAsync(string query, params object[] parameters)
        {
            //ToListAsync is an extension method on IQueryable<T> declared in System.Data.Entity.QueryableExtensions
            return await _context.Movie.FromSql(query).ToListAsync();
        }

        public Movie Find(Guid key, bool readOnly = false)
        {
            var movie = (!readOnly)
                ? _context.Movie.SingleOrDefault(m => m.ID == key)
                //Avoids having the entities be tracked by the context
                : _context.Movie.AsNoTracking().SingleOrDefault(m => m.ID == key)
            ;
            return movie;
        }

        public void Add(Movie item)
        {
            _context.Movie.Add(item);
            _context.SaveChanges();
        }

        public void Remove(Guid key)
        {
            Movie movie = Find(key); ;
            _context.Movie.Remove(movie);
            _context.SaveChanges();
        }
        
        public void Update(Movie item)
        {
            try
            {
                _context.Movie.Update(item);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
            }
        }
    }
}
