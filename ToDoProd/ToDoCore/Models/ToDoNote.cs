using System.ComponentModel.DataAnnotations;

namespace ToDoCore.Models
{
    public class ToDoNote
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsDone { get; set; }
    }
}
