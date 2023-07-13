using System.ComponentModel.DataAnnotations;
namespace RSIapi.Models
{
    public class User
    {
        public int Id { get; set; }

        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string Email { get; set; }

        [Required]
        [Range(1, 100)]
        public int Age { get; set; }

        public string PasswordHash { get; set; }

        public int RoleId { get; set; }

        public virtual Role Role { get; set; }

        public ICollection<ToDoItem> ToDoItems { get; } = new List<ToDoItem>();
    }
}
