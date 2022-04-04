using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiBot.Services
{
    public interface IStockBotService
    {
        Stock GetStock(string stock_code);
    }
}
