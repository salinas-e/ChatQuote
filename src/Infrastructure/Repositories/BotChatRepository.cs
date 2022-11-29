using Domain.Chat.Interfaces;
using Domain.Chat.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BotChatRepository : IBotChatRepository
    {
        private HttpClient _httpClient;
        private string _endpointUrl;

        public string Command => "/stock=";

        public BotChatRepository(string endpointUrl, HttpClient httpClient) 
        {
            _httpClient = httpClient;
            _endpointUrl = endpointUrl;
        }

        public async Task<HttpStatusCode> SendMessage(BotMessage message)
        {
            HttpResponseMessage response;
            var content = new StringContent(JsonSerializer.Serialize<BotMessage>(message), Encoding.UTF8, "application/json");

            try
            {
                response = await _httpClient.PostAsync(_endpointUrl, content);
            }
            catch (Exception)
            {
                return await Task.FromResult(HttpStatusCode.BadRequest);

            }

            return await Task.FromResult(response.StatusCode);
        }
    }
}
