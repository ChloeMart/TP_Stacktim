using System.ComponentModel.DataAnnotations;

namespace StacktimAPI_Chloe.M.DTOs
{
    public class UpdatePlayerDto
    {
        [StringLength(50, MinimumLength = 3)]
        public string? Pseudo { get; set; }

        [EmailAddress, StringLength(100)]
        public string? Email { get; set; }

        [RegularExpression("Bronze|Silver|Gold|Platinum|Diamond|Master")]
        public string? Rank { get; set; }

        public int? TotalScore { get; set; }
    }
}
