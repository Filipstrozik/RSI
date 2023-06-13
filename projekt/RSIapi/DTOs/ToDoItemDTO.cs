using RSIapi.Models;
using System.ComponentModel.DataAnnotations;

namespace RSIapi.DTOs
{
    public class ToDoItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; } = false;
        public DateTime? DueTime { get; set; }

        [Range(1, 5)]
        public int Priority { get; set; } = 3;

        public int BoardId { get; set; }

        public int? UserId { get; set; }
    }
}
