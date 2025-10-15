using System.ComponentModel.DataAnnotations;

namespace StacktimAPI_Chloe.M.DTOs
{
    public class CreatePlayerDto
    {
        [Required, StringLength(50, MinimumLength = 3)]
        public string Pseudo { get; set; }

        [Required, EmailAddress, StringLength(100)]
        public string Email { get; set; }

        [Required]
        [RegularExpression("Bronze|Silver|Gold|Platinum|Diamond|Master")]
        public string Rank { get; set; }
    }
}
