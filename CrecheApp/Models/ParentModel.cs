using System.ComponentModel.DataAnnotations;

namespace CrecheApp.Models
{
    [Serializable]
    public class ParentModel
    {
       
        public int Id { get; set; } 
       
        [StringLength(30,ErrorMessage = "Name is too long or too short",MinimumLength = 4)]
        public string Name { get; set; }
        [StringLength(30, ErrorMessage = "Name is too long or too short", MinimumLength = 4)]
        public string Surname { get; set; }
        [Display(Name = "ID Number")]
        [StringLength(13, ErrorMessage = "ID number is incorrect", MinimumLength = 13)]
        [Range(0,9999999999999)]
        public string IDNumber { get; set; }
        [StringLength(300,ErrorMessage = "Address too short or too long",MinimumLength = 15)]
        public string Address { get; set; }
        [StringLength(10, ErrorMessage = "Phone Number Incorrect", MinimumLength = 10)]
        [Range(0, 9999999999)]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
        [StringLength(256)]
        public string UserId { get; set; }


    }
}
