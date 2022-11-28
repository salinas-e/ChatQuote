using Domain.Chat.Models;
using Domain.Chat.SeedWork.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebChat.ViewModels
{
    public class SelectedRoomViewModel
    {
        public string ChatUrl { get; private set; }
        public ChatRoom ChatRoom { get; private set; }

        public SelectedRoomViewModel(int roomId, ChatSettings chatSettings)
        {
            ChatRoom = chatSettings.ChatRooms.FirstOrDefault(x => x.Id == roomId);
            ChatUrl = chatSettings.ChatUrl;
            
        }
    }
}
