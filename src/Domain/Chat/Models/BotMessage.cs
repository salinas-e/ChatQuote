using Domain.Chat.SeedWork.Enums;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Domain.Chat.Models
{
    public class BotMessage
    {
        public string Code { get; private set; }
        public string ConnectionId { get; private set; }
        public string Room { get; private set; }

        public BotMessage(string code, string connectionId, string room)
        {
            Code = code;
            ConnectionId= connectionId;
            Room = room;
        }
    }
}
