using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Domain.Chat.Models
{

    public sealed class ChatRoom
   {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public sealed class ChatSettings
    {
        public string ChatUrl { get; set; }
        public string QuoteBotUrl { get; set; }

        public ICollection<ChatRoom> ChatRooms { get; set; }
    }
}
