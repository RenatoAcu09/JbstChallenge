


using Entity.Models;

namespace WebChat.Services
{
    public interface IStockBotService
    {
        BotResponse BotDetection(string message);
    }
}