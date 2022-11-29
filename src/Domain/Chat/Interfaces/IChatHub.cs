using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Chat.Interfaces
{
    public interface IChatHub
    {
        Task SendMessage(string room, string user, string connectionId, string message);
        Task JoinChatRoom(string room);
    }
}
