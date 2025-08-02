using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Mvc;
namespace Todo_App.Models
{
    [Table("Todos")]
    public class TodoItem
    {
        [Key]
        public int Id { get; set; }
        public string? Title{ get; set; }
        public bool IsCompleted{ get; set; }
        public string? Description { get; set; }



    }
}
