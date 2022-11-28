using Domain.Chat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebChat.ViewModels
{
    public class ChatRoomsViewModel
    {
        public ICollection<ChatRoom> ChatRooms { get; set; }
    }
}
