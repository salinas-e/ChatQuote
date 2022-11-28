using Domain.Chat.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Chat.Interfaces
{
    public interface IChatRepository
    {
        Task JoinRoom();
        Task SendMessage(Message message);
    }
}
