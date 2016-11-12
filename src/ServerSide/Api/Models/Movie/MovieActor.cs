using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Movie
{
    public class MovieActor
    {
        public Guid MovieID { get; set; }
        public Movie Movie { get; set; }

        public Guid ActorID { get; set; }
        public Actor Actor { get; set; }
    }
}
