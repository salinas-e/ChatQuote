using Domain.Chat.SeedWork.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Chat.Models
{
    public class Message
    {
        public MessageTypes MessageType { get; private set; }
        public string Text { get; private set; }

        public Message(MessageTypes messageType, string text)
        {
            MessageType = messageType;
            Text = text;
        }
    }
}
