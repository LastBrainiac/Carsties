﻿using Contracts;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using NotificationService.Hubs;

namespace NotificationService.Consumers
{
    public class AuctionCreatedConsumer(IHubContext<NotificationHub> _hubContext) : IConsumer<AuctionCreated>
    {
        public async Task Consume(ConsumeContext<AuctionCreated> context)
        {
            Console.WriteLine("==> auction created message received");

            await _hubContext.Clients.All.SendAsync("AuctionCreated", context.Message);
        }
    }
}