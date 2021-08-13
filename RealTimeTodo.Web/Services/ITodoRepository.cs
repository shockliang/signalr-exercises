using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealTimeTodo.Web.Models;

namespace RealTimeTodo.Web.Services
{
    public interface ITodoRepository
    {
        Task<List<ToDoListMinimal>> GetList();
    }

    public class InMemoryToDoRepository : ITodoRepository
    {
        private static List<ToDoList> Lists { get; set; } = new List<ToDoList>();

        static InMemoryToDoRepository()
        {
            Lists.Add(new ToDoList {Id = 0, Name = "Foo", Items = new List<ToDoItem>()});
            Lists.Add(new ToDoList {Id = 1, Name = "Walking", Items = new List<ToDoItem>()});
            Lists.Add(new ToDoList {Id = 2, Name = "Study", Items = new List<ToDoItem>()});
            Lists.Add(new ToDoList {Id = 3, Name = "Bar", Items = new List<ToDoItem>()});
            Lists.Add(new ToDoList {Id = 4, Name = "Eating", Items = new List<ToDoItem>()});
        }

        public Task<List<ToDoListMinimal>> GetList()
        {
            return Task.FromResult(Lists.Select(x => x.GetMinimal()).ToList());
        }
    }
}