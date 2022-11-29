using Domain.Chat.Interfaces;
using Domain.Chat.Models;
using Domain.Chat.SeedWork.Enums;
using Infrastructure.Extensions;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Hubs
{
    public class ChatHub : Hub, IChatHub
    {
        private IBotChatRepository _botChatRepository;

        public ChatHub(IBotChatRepository botChatRepository)
        {
            _botChatRepository = botChatRepository;
        }

        public async Task SendMessage(string room, string user, string connectionId, string message)
        {
            var botCommand = _botChatRepository.Command;

            if (message.GetMessageType(botCommand) == MessageTypes.Command)
            {
                var botMessage = new BotMessage(message.RemoveCommand(botCommand), connectionId, room); //Todo Erick: pass de connId and userId
                
                var statusCodeResponse = await _botChatRepository.SendMessage(botMessage);

                if (statusCodeResponse != HttpStatusCode.OK)
                    message = "There was an issue, try again!";
                else
                    message = "Command issued";

                await Clients.Caller.SendAsync("ReceivedMessage", "CHAT-BOT", message);
            }
            else
            {
                await Clients.Groups(room).SendAsync("ReceivedMessage", user, message);
            }
        }

        public async Task JoinChatRoom(string room)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, room);
            await Clients.Group(room).SendAsync(HubMethodNames.JoinChatGroup.ToString(), $"new user connected: {Context.User.Identity}");
        }
    }
}
