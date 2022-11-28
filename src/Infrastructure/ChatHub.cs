using Domain.Chat.Interfaces;
using Domain.Chat.SeedWork.Enums;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ChatHub : Hub, IChatHub
    {
        public async Task SendMessage(string room, string user, string message)
        {
            await Clients.Groups(room).SendAsync("ReceivedMessage", user, message);
        }

        public async Task JoinChatRoom(string room)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, room);
            await Clients.Group(room).SendAsync(HubMethodNames.JoinGroup.ToString(), $"new user connected: {Context.User.Identity}");
        }
    }
}
