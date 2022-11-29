using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Chat.Models;
using Infrastructure.Hubs;

namespace Infrastructure.Extensions
{
    public static class StartupExtensions
    {
        public static IEndpointRouteBuilder ConfigureChatHub(this IEndpointRouteBuilder endpoint, IConfiguration configuration)
        {
            endpoint.MapHub<ChatHub>(configuration.GetSection("ChatConfiguration").GetSection("ChatUrl").Value);

            return endpoint;
        }

        public static IServiceCollection AddChatService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ChatSettings>(configuration.GetSection("ChatConfiguration"));

            services.AddSignalR();

            return services;
        }
    }
}