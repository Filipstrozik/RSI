using System.ComponentModel.DataAnnotations;
namespace RSIapi.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public ICollection<ToDoItem> ToDoItems { get; } = new List<ToDoItem>();
    }
}
