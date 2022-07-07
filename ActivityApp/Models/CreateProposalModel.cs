using System.ComponentModel.DataAnnotations;

namespace ActivityApp.Models
{
    public class CreateProposalModel
    {
        [Required]
        [MinLength(15)]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        [MinLength(1)]
        [Display(Name = "Activity")]
        public string ActivityId { get; set; }
        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        public DateTime ActivityDate { get; set; } = DateTime.Now;
    }
}
