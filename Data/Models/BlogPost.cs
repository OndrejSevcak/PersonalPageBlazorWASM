using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class BlogPost
    {
        public string Id { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public List<Tag> Tags { get; set; }
        public string Content { get; set; }
    }
}
