using BotAPI.Extensions;
using BotAPI.Models;
using BotAPI.ResponseModel;
using BotAPI.Services;
using CsvHelper;
using CsvHelper.Configuration;
using Domain.Chat.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Formats.Asn1;
using System.Globalization;
using static System.Net.WebRequestMethods;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(hc => new HttpClient { BaseAddress = new Uri("https://stooq.com/") });

var awsCredentialsSettings = new AwsCredentialsSettings();

builder.Configuration.GetSection("AwsCredentials").Bind(awsCredentialsSettings);
builder.Services.AddScoped<IAwsSqsService>(hc => new AwsSqsService(awsCredentialsSettings));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/stockquote",async (BotMessage botMessage, HttpClient httpClient, IAwsSqsService awsSqsService) =>
{
    var req = new HttpRequestMessage(HttpMethod.Get, $"/q/l?s={botMessage.Code}&f=sd2t2ohlcv&h&e=csv");

    var response = await httpClient.SendAsync(req);

    response.EnsureSuccessStatusCode();

    var stockQuote = response.ParseCsvResponseToObject<StockQuoteResponse>();

    return await awsSqsService.SendMessage(stockQuote);
})
.WithName("GetStockQuote")
.WithOpenApi();

app.Run();


