using BotAPI.Extensions;
using BotAPI.ResponseModel;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Hosting;
using System.Formats.Asn1;
using System.Globalization;
using static System.Net.WebRequestMethods;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(hc => new HttpClient { BaseAddress = new Uri("https://stooq.com/") });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/stockquote/{stock_code}",async (string stock_code,HttpClient httpClient) =>
{
    var req = new HttpRequestMessage(HttpMethod.Get, $"/q/l?s={stock_code}&f=sd2t2ohlcv&h&e=csv");

    var response = await httpClient.SendAsync(req);

    response.EnsureSuccessStatusCode();

    return response.ParseCsvResponseToObject<StockQuoteResponse>();
})
.WithName("GetStockQuote")
.WithOpenApi();

app.Run();


