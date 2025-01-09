using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Autokereskedes.Models
{
    public class Favourite
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int AutoId { get; set; }

        public Auto Auto { get; set; }
    }
}
