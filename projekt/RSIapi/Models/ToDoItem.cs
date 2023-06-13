using System.ComponentModel.DataAnnotations;

namespace RSIapi.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public bool IsComplete { get; set; }
        public DateTime? DueTime { get; set; }

        [Range(1, 5)]
        public int Priority { get; set; }
    }
}
