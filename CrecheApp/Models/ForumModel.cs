using System.ComponentModel.DataAnnotations;

namespace CrecheApp.Models
{
    public class ForumModel
    {
        [Key]
        public String CommentId { get; set; }

        public DateTime CreatedDate { get; set; }
        [Required]
        [StringLength(300,ErrorMessage ="Message is either too long or too short",MinimumLength =10)]
        public String Comment { get; set; }
        public String UserId { get; set; }
       
        public ForumModel()
        {
            CommentId = Guid.NewGuid().ToString();
        }


    }
}
