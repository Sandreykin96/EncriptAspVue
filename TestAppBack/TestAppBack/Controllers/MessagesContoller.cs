using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestAppBack.DbHelpers;
using TestAppBack.Helpers;

namespace TestAppBack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly ILogger<MessagesController> _logger;
        IWebHostEnvironment _appEnvironment;
        private ApplicationContext db;
        private DbEncoder encoder { get; set; }

        public MessagesController(ILogger<MessagesController> logger, IWebHostEnvironment appEnvironment, ApplicationContext context)
        {
            _logger = logger;
            _appEnvironment = appEnvironment;
            db = context;

            CaesarEncoderTableCreator creator = new CaesarEncoderTableCreator(context);//оставил временно тут, т.к. насколько понял по условиям -
            creator.CreateKeysTable();                                                 //таблицу с ключами вручную создаем перед первым запуском 

            encoder = new DbEncoder(db);
        }

        [HttpGet]
        public async Task<IEnumerable<Message>> Get()
        {
            var messages = db.Messages.ToList();
            messages.ForEach(message => message.Title = encoder.Encode(message.Title));
            return messages;
        }

        [HttpPost]
        public async Task<Message> Post(object titleInfo)
        {
            Message recentMessage = new Message();
            recentMessage.Date = DateTime.Now;
            recentMessage.Completed = false;
            recentMessage.Title = titleInfo.ToString();
            db.Messages.Add(recentMessage);
            await db.SaveChangesAsync();
            recentMessage.Title = encoder.Encode(titleInfo.ToString());
            return recentMessage;
        }
    }
}