using System.Collections.Generic;
using System.Threading.Tasks;
using RealTimeTodo.Web.Models;

namespace RealTimeTodo.Web.Services
{
    public interface ITodoRepository
    {
        Task<List<ToDoList>> GetList();
    }

    public class InMemoryToDoRepository : ITodoRepository
    {
        private static List<ToDoList> Lists { get; set; } = new List<ToDoList>();

        public Task<List<ToDoList>> GetList()
        {
            return Task.FromResult(Lists);
        }
    }
}