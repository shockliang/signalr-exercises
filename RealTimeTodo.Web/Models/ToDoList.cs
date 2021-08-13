using System.Collections.Generic;
using System.Linq;

namespace RealTimeTodo.Web.Models
{
    public class ToDoList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ToDoItem> Items { get; set; }
        public int Pending => Items.Count(x => !x.IsCompleted);
        public int Completed => Items.Count(x => x.IsCompleted);

        public ToDoListMinimal GetMinimal()
        {
            return new()
            {
                Id = Id,
                Name = Name,
                Pending = Pending,
                Completed = Completed
            };
        }
    }
}