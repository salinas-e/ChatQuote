using Domain.Chat.SeedWork.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Extensions
{
    internal static class StringExtensions
    {
        public static MessageTypes GetMessageType(this string message, string botCommand)
        {
            return message.StartsWith(botCommand) ? MessageTypes.Command : MessageTypes.Text;
        }

        public static string RemoveCommand(this string message, string botCommand)
        {
            return message = message.Replace(botCommand, "");
        }
    }
}
