using System.ComponentModel.DataAnnotations;

namespace CrecheApp.Models
{
    public class ForumReplyModel
    {
        [Key]
        public String ReplyId { get; set; }
        public String CommentId { get; set; }
        public String Reply { get; set; }
        public DateTime Created { get; set; }

    }
}
