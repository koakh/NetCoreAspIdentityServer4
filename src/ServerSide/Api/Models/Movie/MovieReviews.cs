using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models.Movie
{
    [Table("moviereview")]
    public class MovieReview : BaseEntity
    {
        //The ID field is required by the DB for the primary key
        public Guid ID { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [StringLength(1024)]
        public string Review { get; set; }

        [Required]
        public Movie Movie { get; set; }
    }
}
