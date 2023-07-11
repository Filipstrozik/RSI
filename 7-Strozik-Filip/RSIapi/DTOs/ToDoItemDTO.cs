using System.ComponentModel.DataAnnotations;

namespace RSIapi.DTOs
{
    public class ToDoItemDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        public bool IsComplete { get; set; } = false;

        [Required]
        public DateTime DueTime { get; set; }

        [Required]
        [Range(1, 5)]
        public int Priority { get; set; } = 3;

        [Required]
        public int BoardId { get; set; }

        public int? UserId { get; set; }
    }
}
