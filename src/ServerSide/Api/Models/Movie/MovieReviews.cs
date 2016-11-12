using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Models.Movie
{
    public class MovieReviews
    {
        //The ID field is required by the DB for the primary key
        public Guid ID { get; set; }

        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [StringLength(1024)]
        public string Review { get; set; }
    }
}
