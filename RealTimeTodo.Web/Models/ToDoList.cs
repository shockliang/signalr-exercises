using System.Collections.Generic;

namespace RealTimeTodo.Web.Models
{
    public class ToDoList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ToDoItem> Items { get; set; }
    }
}