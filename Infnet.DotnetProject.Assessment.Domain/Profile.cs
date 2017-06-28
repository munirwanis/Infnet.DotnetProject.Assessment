using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infnet.DotnetProject.Assessment.Domain
{
    [Table("Profile")]
    public class Profile
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "User ID")]
        public string UserId { get; set; }

        [Display(Name = "User Email")]
        public string UserEmail { get; set; }

        [Display(Name = "Profile Picture")]
        public string ProfilePicture { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Nickname")]
        public string NickName { get; set; }

    }
}
