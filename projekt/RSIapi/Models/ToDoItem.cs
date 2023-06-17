using System.ComponentModel.DataAnnotations;

namespace RSIapi.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public bool IsComplete { get; set; } = false;

        private DateTime _dueTime;

        public DateTime DueTime
        {
            get { return _dueTime.ToUniversalTime(); }
            set { _dueTime = value.ToLocalTime(); }
        }

        [Range(1, 5)]
        public int Priority { get; set; } = 3;

        public Board Board { get; set; }

        public User? User { get; set; }
    }
}
