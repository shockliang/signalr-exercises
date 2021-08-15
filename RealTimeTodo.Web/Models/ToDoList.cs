using System;
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

        public void AddItem(string text)
        {
            var id = Items.Any()
                ? Items.Max(x => x.Id) + 1
                : 0;
            Items.Add(new ToDoItem
            {
                Text = text,
                Id = id
            });
        }

        public void Toggle(int itemId)
        {
            var item = Items.FirstOrDefault(x => x.Id.Equals(itemId));
            if (item == null) throw new NullReferenceException("Invalid item id");
            item.IsCompleted = !item.IsCompleted;
        }
    }
}