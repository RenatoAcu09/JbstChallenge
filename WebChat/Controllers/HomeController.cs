using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebChat.Data;
using WebChat.Models;

namespace WebChat.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public readonly ApplicationDbContext _context;
        public readonly UserManager<AppUser> _userManager;
        //public IHubContext<ChatHub> _hubContext;

        public HomeController(ApplicationDbContext context, UserManager<AppUser> userManager)
        //, IHubContext<ChatHub> hubContext)
        {
            _context = context;
            _userManager = userManager;
            //_hubContext = hubContext;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    ViewBag.CurrentUserName = currentUser.UserName;
                }
                var messages = await _context.Messages.OrderByDescending(m => m.When).Take(50).ToListAsync();
                return View(messages);
            }
            return View();
        }

        public async Task<IActionResult> Create(Message message)
        {
            if (ModelState.IsValid)
            {
                message.UserName = User.Identity.Name;
                var sender = await _userManager.GetUserAsync(User);
                message.UserID = sender.Id;
                await _context.Messages.AddAsync(message);
                await _context.SaveChangesAsync();
                //await _hubContext.Clients.All.SendAsync("receiveMessage", message);
                return Ok();
            }
            return Error();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
