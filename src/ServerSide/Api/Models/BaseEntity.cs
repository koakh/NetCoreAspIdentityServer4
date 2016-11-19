using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    [NotMapped]
    public class BaseEntity
    {
        [Column(Order = 100)]
        public DateTime? DateCreated { get; set; }
        [Column(Order = 110)]
        public DateTime? DateModified { get; set; }

        //[Column(Order = 120)]
        //public string UserCreated { get; set; }
        //[Column(Order = 130)]
        //public string UserModified { get; set; }
    }
}
