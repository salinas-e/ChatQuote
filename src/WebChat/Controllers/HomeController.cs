using Domain.Chat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebChat.Models;
using WebChat.ViewModels;

namespace WebChat.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ChatSettings _chatSettings;

        public HomeController(ILogger<HomeController> logger, IOptions<ChatSettings> chatOptions)
        {
            _logger = logger;
            _chatSettings = chatOptions.Value;
        }

        public IActionResult Index()
        {
            var model = new ChatRoomsViewModel();
            model.ChatRooms = _chatSettings.ChatRooms;

            return View(model);
        }

        public IActionResult Room(int roomId)
        {
            var model = new SelectedRoomViewModel(roomId, _chatSettings);

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
