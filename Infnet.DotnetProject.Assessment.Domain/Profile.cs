using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infnet.DotnetProject.Assessment.Domain
{
    [Table("Profile")]
    public class Profile
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        public string UserEmail { get; set; }

        public string ProfilePicture { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string NickName { get; set; }

    }
}
