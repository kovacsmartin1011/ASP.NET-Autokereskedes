using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Autokereskedes.Models
{
    public class Profile
    {
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber {  get; set; }
        public string Address { get; set; }

        [DataType(DataType.Date)] //
        public DateTime? BirthDate { get; set; }
        public string ProfilePictureUrl { get; set; }

        [Key] //elsődleges kulcs
        public string? UserId { get; set; }
    }
}
