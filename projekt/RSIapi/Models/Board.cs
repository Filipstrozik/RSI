using System.ComponentModel.DataAnnotations;
namespace RSIapi.Models
{
    public class Board
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }

        private DateTime _dueTime;
        public DateTime DueTime
        {
            get { return _dueTime.ToUniversalTime(); }
            set { _dueTime = value.ToLocalTime(); }
        }

        public ICollection<ToDoItem> ToDoItems { get; } = new List<ToDoItem>();
    }
}
