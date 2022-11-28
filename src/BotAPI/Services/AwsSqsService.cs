using System;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using BotAPI.Models;
using BotAPI.ResponseModel;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BotAPI.Services
{
    public interface IAwsSqsService
    {
        Task<HttpStatusCode> SendMessage(StockQuoteResponse stockQuote);
    }

    public class AwsSqsService : IAwsSqsService
    {
        private AwsCredentialsSettings _awsSettings;
        public AwsSqsService(AwsCredentialsSettings awsCredentialsSettings)
        {
            _awsSettings = awsCredentialsSettings;
        }

        public async Task<HttpStatusCode> SendMessage(StockQuoteResponse stockQuote)
        {
            string messageBody = JsonSerializer.Serialize(stockQuote);

            IAmazonSQS client = new AmazonSQSClient(_awsSettings.AccesKey, _awsSettings.SecretAccessKey, RegionEndpoint.USEast1);

            var request = new SendMessageRequest
            {
                MessageBody =messageBody,
                QueueUrl = _awsSettings.SqsUrl
            };

            var response = await client.SendMessageAsync(request);

            return response.HttpStatusCode;
        }
    }
}
