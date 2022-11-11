using System.ComponentModel.DataAnnotations;

namespace CrecheApp.Models
{
    public class ForumModel
    {
        [Key]
        public int CommentId { get; set; } 

        public DateTime CreatedDate { get; set; }
        [Required]
        [StringLength(300,ErrorMessage = "Too long or too short",MinimumLength = 10)]
        public String Comment { get; set; }
        public String UserId { get; set; }
       
       


    }
}
