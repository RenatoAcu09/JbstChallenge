
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Entity.Models;

namespace WebChat.Services
{
    public class StockBotService : IStockBotService
    {
        private HttpClient client { get; set; }

        public StockBotService(HttpClient _client)
        {
            client = _client;
        }

        public BotResponse BotDetection(string message)
        {
            try
            {
                if (message.ToLower().Contains("/stock="))
                {
                    string code = message.Replace("/stock=", "");
                    using (HttpResponseMessage response = client.GetAsync($"https://localhost:44345/api/StockApi/GetStock?stock_code={code}").Result)
                    using (HttpContent content = response.Content)
                    {
                        string serviceResponse = content.ReadAsStringAsync().Result;
                        if (response.StatusCode != System.Net.HttpStatusCode.OK)
                            return new BotResponse { Detected = true, IsSuccessful = false, ErrorMessage = response.StatusCode.ToString()};

                        var stock = JsonConvert.DeserializeObject<Stock>(serviceResponse);
                        return new BotResponse { Detected = true, IsSuccessful = true, Symbol = stock.Symbol, Close = stock.Close.ToString() };
                    }
                }
                return new BotResponse { Detected = false };
            }
            catch (Exception ex)
            {
                return new BotResponse { Detected = true, IsSuccessful = false, ErrorMessage = ex.Message};
            }
        }
    }
}
