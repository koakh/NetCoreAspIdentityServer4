using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Blog
{
    [Table("tag")]
    public class Tag
    {
        //The ID field is required by the DB for the primary key
        public string ID { get; set; }

        public List<PostTag> PostTags { get; set; }
    }
}
