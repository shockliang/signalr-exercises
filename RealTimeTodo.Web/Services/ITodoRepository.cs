using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealTimeTodo.Web.Models;

namespace RealTimeTodo.Web.Services
{
    public interface ITodoRepository
    {
        Task<List<ToDoListMinimal>> GetList();
        Task<ToDoList> GetList(int id);
        Task AddToDoItem(int listId, string text);
        Task ToggleToDoItem(int listId, int itemId);
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

        public Task<ToDoList> GetList(int id)
        {
            return Task.FromResult(Lists.FirstOrDefault(x => x.Id.Equals(id)));
        }

        public async Task AddToDoItem(int listId, string text)
        {
            var getList = await GetList(listId);
            if (getList == null) throw new NullReferenceException("Invalid list id");
            getList.AddItem(text);
        }

        public async Task ToggleToDoItem(int listId, int itemId)
        {
            var getList = await GetList(listId);
            if (getList == null) throw new NullReferenceException("Invalid list id");
            getList.Toggle(itemId);
        }
    }
}