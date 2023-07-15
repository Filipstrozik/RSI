using RSIapi.Models;
using System.ComponentModel.DataAnnotations;

namespace RSIapi.DTOs
{
    public class CreateBoardDto
    {
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
    }
}
