using Microsoft.AspNetCore.SignalR;

namespace SignalIRChat.Hubs
{
    public class ChatHub: Hub
    {
        //add user connection to a spesific group
        public async Task JoinRoom(string roomName, string userName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            await Clients.Group(roomName).SendAsync("ReceiveSystemMessage", $"{userName} has joined the room.");
        }

        //remove user connection from a spesific group
        public async Task LeaveRoom(string roomName, string userName) {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
            await Clients.Group(roomName).SendAsync("ReceiveSystemMessage", $"User {userName} has left the room");
        }

        //send message only to clients inside a speific room
        public async Task SendMessage(string roomName, string userName, string message) 
        {
            await Clients.Group(roomName).SendAsync("ReceiveGroupMessage", userName, message);
        }
    }
}
