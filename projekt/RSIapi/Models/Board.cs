namespace RSIapi.Models
{
    public class Board
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueTime { get; set; }
        public ICollection<ToDoItem> ToDoItems { get; } = new List<ToDoItem>();
    }
}
