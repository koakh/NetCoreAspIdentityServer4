using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Blog
{
    [Table("post")]
    public class Post
    {
        //The ID field is required by the DB for the primary key
        public Guid ID { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }

        public List<PostTag> PostTags { get; set; }
    }
}
