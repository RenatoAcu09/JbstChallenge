using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebChat.Models;
using WebChat.Services;

namespace WebChat.Hubs
{
    public class ChatHub : Hub
    {

        private readonly IStockBotService StockBotService;

        public ChatHub(IStockBotService _stockBotService)
        {
            StockBotService = _stockBotService;
        }

        public async Task SendMessage(Message message)
        {
            var botResponse = StockBotService.BotDetection(message.Text);
            if (botResponse.Detected)
            {
                if (botResponse.IsSuccessful)
                {
                    await Clients.All.SendAsync("receiveMessage", StockBotMessage($"{botResponse.Symbol} quote is ${botResponse.Close} per share"));
                }
                else
                {
                    await Clients.All.SendAsync("receiveMessage", StockBotMessage($"Bot have a problem. { botResponse.ErrorMessage }"));
                }
            }
            else
            {
                await Clients.All.SendAsync("receiveMessage", message);
            }
        }


        internal Message StockBotMessage(string text)
        {
            return new Message
            {
                UserName = "StockBot",
                Text = text,
                When = DateTime.Now
            };
        }
    }
}
