using Domain.Chat.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Chat.Interfaces
{
    public interface IBotChatRepository
    {
        string Command { get; }
        Task<HttpStatusCode> SendMessage(BotMessage message);
    }
}
