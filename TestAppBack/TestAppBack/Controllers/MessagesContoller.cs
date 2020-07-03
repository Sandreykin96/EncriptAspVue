using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TestAppBack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly ILogger<MessagesController> _logger;

        IWebHostEnvironment _appEnvironment;
        private ApplicationContext db;

        public MessagesController(ILogger<MessagesController> logger, IWebHostEnvironment appEnvironment, ApplicationContext context)
        {
            _logger = logger;
            _appEnvironment = appEnvironment;
            db = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Message>> Get()
        {
            return db.Messages.ToList();
        }

        [HttpPost]
        public async Task<Message> Post(object titleInfo)
        {
            Message recentMessage = new Message();
            string Titile = titleInfo.ToString();
            recentMessage.Date = DateTime.Now;
            recentMessage.Completed = false;
            recentMessage.Title = Titile;
            db.Messages.Add(recentMessage);
            await db.SaveChangesAsync();
            return recentMessage;
        }
    }
}