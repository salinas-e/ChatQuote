using Domain.Chat.Models;
using Infrastructure.Repositories;
using RichardSzalay.MockHttp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tests.Repositories
{
    public class BotChatRepositoryTests
    {
        private const string BASE_URL = "https://localhost";

        private MockHttpMessageHandler _mockHttpMessage;
        private BotMessage _botMessageSample;

        public BotChatRepositoryTests()
        {
            _mockHttpMessage = new MockHttpMessageHandler();

            _botMessageSample = new BotMessage("aapl.us", "kjaljsdjfa9090ewrqljlk", "room01");
        }


        [Fact]
        public async void SendMessage_With_Bot_Running_Should_Return_StatusCode_OK()
        {
            //Arrange
            HttpStatusCode httpStatusCode = HttpStatusCode.OK;
            var endpointUrl = "/stockquote";
            var botMessage = _botMessageSample;

            _mockHttpMessage.When($"{BASE_URL}{endpointUrl}")
                    .Respond(httpStatusCode);

            var botChatRepository = new BotChatRepository(endpointUrl, 
                                    new HttpClient(_mockHttpMessage) { BaseAddress = new Uri(BASE_URL) });

            //Act
            var response = await botChatRepository.SendMessage(botMessage);

            //Assert
            Assert.True(response == httpStatusCode);
        }

        [Fact]
        public async void SendMessage_With_Bot_NOT_Running_Should_Return_StatusCode_BadRequest()
        {
            //Arrange
            HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest;
            var endpointUrl = "/stockquote";
            var botMessage = _botMessageSample;

            _mockHttpMessage.When($"{BASE_URL}{endpointUrl}").
                            Throw(new Exception("Service not available"));

            var botChatRepository = new BotChatRepository(endpointUrl,
                                    new HttpClient(_mockHttpMessage) { BaseAddress = new Uri(BASE_URL) });

            //Act
            var response = await botChatRepository.SendMessage(botMessage);

            //Assert
            Assert.True(response == httpStatusCode);
        }
    }
}
