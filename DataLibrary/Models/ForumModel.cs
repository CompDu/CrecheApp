using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary
{
    public class ForumModel
    {
        public string CommentId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Comment { get; set; }
        public string UserId { get; set; }
    }
}
