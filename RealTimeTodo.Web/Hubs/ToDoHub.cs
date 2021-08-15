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

        public async Task GetLists()
        {
            var result = await todoRepository.GetLists();
            await Clients.Caller.SendAsync("updatedToDoList", result);
        }

        public async Task GetList(int listId)
        {
            var result = await todoRepository.GetList(listId);
            await Clients.Caller.SendAsync("updatedListData", result);
        }

        public async Task SubscribeToCountUpdates()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "Counts");
        }

        public async Task UnsubscribeToCountUpdates()
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Counts");
        }

        public async Task SubscribeToListUpdates(int listId)
        {
            var groupName = ListIdToGroupName(listId);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task UnsubscribeToListUpdates(int listId)
        {
            var groupName = ListIdToGroupName(listId);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task AddToDoItem(int listId, string text)
        {
            await todoRepository.AddToDoItem(listId, text);
            var allLists = await todoRepository.GetLists();
            var updateList = await todoRepository.GetList(listId);

            var groupName = ListIdToGroupName(listId);
            await Clients.Group("Counts").SendAsync("updatedToDoList", allLists);
            await Clients.Group(groupName).SendAsync("updatedListData", updateList);
        }
        
        public async Task ToggleToDoItem(int listId, int itemId)
        {
            await todoRepository.ToggleToDoItem(listId, itemId);
            var allLists = await todoRepository.GetLists();
            var updateList = await todoRepository.GetList(listId);

            var groupName = ListIdToGroupName(listId);
            await Clients.Group("Counts").SendAsync("updatedToDoList", allLists);
            await Clients.Group(groupName).SendAsync("updatedListData", updateList);
        }
        
        private string ListIdToGroupName(int listId) => $"group-list-{listId}";
    }
}