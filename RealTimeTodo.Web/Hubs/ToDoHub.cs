using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using RealTimeTodo.Web.Models;
using RealTimeTodo.Web.Services;

namespace RealTimeTodo.Web.Hubs
{
    public class ToDoHub : Hub
    {
        private readonly ITodoRepository todoRepository;

        public ToDoHub(ITodoRepository todoRepository)
        {
            this.todoRepository = todoRepository;
        }

        public Task<List<ToDoList>> GetLists()
        {
            return todoRepository.GetList();
        }
    }
}