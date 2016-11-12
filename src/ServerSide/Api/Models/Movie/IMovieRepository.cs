using Api.Data;
using System;
using System.Collections.Generic;

namespace Api.Models.Movie
{
    public interface IMovieRepository : IRawSql<Movie>
    {
        IEnumerable<Movie> GetAll();
        void Add(Movie item);
        Movie Find(Guid key, bool readOnly = false);
        void Remove(Guid key);
        void Update(Movie item);
    }
}
