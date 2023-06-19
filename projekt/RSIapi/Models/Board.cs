using System.ComponentModel.DataAnnotations;
namespace RSIapi.Models
{
    public class Board
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        [MinLength(3)]
        public string Description { get; set; }

        private DateTime _dueTime;

        [Required]
        public DateTime DueTime
        {
            get { return _dueTime.ToUniversalTime(); }
            set { _dueTime = value.ToLocalTime(); }
        }

        public ICollection<ToDoItem> ToDoItems { get; } = new List<ToDoItem>();
    }
}
