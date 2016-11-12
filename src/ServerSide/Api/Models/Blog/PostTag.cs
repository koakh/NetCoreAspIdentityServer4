using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Blog
{
    public class PostTag
    {
        public Guid PostID { get; set; }
        public Post Post { get; set; }

        public string TagID { get; set; }
        public Tag Tag { get; set; }
    }
}
