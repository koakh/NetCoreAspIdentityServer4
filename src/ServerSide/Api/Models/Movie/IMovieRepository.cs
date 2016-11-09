using Api.Data;
using System.Collections.Generic;

namespace Api.Models.Movie
{
    public interface IMovieRepository :  IRawSql<Movie>
    {
        IEnumerable<Movie> GetAll();
        void Add(Movie item);
        Movie Find(int key, bool readOnly = false);
        void Remove(int key);
        void Update(Movie item);
    }
}
