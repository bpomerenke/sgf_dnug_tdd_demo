using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TDDDemoApp.DAL;
using TDDDemoApp.DAL.Entities;

namespace TDDDemoApp.Controllers
{
    public class MessageController : ApiController
    {
        private readonly IRepository _repository;

        public MessageController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("api/v1/message")]
        public MessageView SaveMessage(MessageView messageView)
        {
            var message = new MessageEntity{Message = messageView.Message};

            _repository.Insert(message);
            _repository.SaveChanges();

            return new MessageView
            {
                Id = message.Id,
                CreatedDateTime = message.CreatedDateTime,
                Message = message.Message
            };
        }

        [HttpGet]
        [Route("api/v1/messages")]
        public List<MessageView> GetAll()
        {
            return _repository.FindAll<MessageEntity>()
                .OrderByDescending(x => x.CreatedDateTime)
                .Select(x => new MessageView
                {
                    Id = x.Id,
                    Message = x.Message,
                    CreatedDateTime = x.CreatedDateTime
                }).ToList();
        }

        public class MessageView
        {
            public int Id { get; set; }
            public string Message { get; set; }
            public DateTime CreatedDateTime { get; set; }
        }
    }
}