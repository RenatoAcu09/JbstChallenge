using Entity.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using WebApiBot.Services;

namespace WebApiBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockApiController : ControllerBase
    {
        private IStockBotService StockBotService;

        public StockApiController(IStockBotService stockInfoDomain)
        {
            StockBotService = stockInfoDomain;
        }

        [HttpGet]
        [Route("GetStock")]
        public ActionResult<Stock> GetStock(string stock_code)
        {
            try
            {
                var result = StockBotService.GetStock(stock_code);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

