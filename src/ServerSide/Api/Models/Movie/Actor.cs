using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models.Movie
{
    public class Actor : BaseEntity
    {
        //The ID field is required by the DB for the primary key
        public Guid ID { get; set; }

        [Column(Order = 10)]
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Column(Order = 20)]
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string LastName { get; set; }

        [Column(Order = 30)]
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Born Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BornDate { get; set; }

        [Column(Order = 40)]
        [Required]
        [Range(1, 120)]
        public int Age { get; set; }

        public List<MovieActor> MovieActors { get; set; }
    }
}
