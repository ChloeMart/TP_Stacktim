using System.ComponentModel.DataAnnotations;

namespace StacktimAPI_Chloe.DTOs
{
    public class CreateTeamDto
    {
        [Required, StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [Required, StringLength(3, MinimumLength = 3), RegularExpression("[A-Z][A-Z][A-Z]")]
        public string Tag { get; set; }

        [Required]
        public int CaptainId { get; set; }

    }
}
