using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using TDDDemoApp.Controllers;
using TDDDemoApp.DAL;
using TDDDemoApp.DAL.Entities;

namespace TDDDemoApp.UnitTest.Controllers
{
    [TestFixture]
    public class MessageControllerFixture
    {
        private MessageController _testObject;

        private Mock<IRepository> _repository;

        [SetUp]
        public void Setup()
        {
            _repository = new Mock<IRepository>();
            _testObject = new MessageController(_repository.Object);
        }

        class SaveMessage : MessageControllerFixture
        {
            [Test]
            public void Inserts_Message_And_Saves()
            {
                //TODO
            }

            [Test]
            public void Returns_MessageView_With_Id_And_CreatedDate()
            {
                //TODO
            }
        }

        class GetAll : MessageControllerFixture
        {
            [Test]
            public void Returns_Existing_Message()
            {
                var existingMessage = new MessageEntity
                {
                    Id = 15,
                    CreatedDateTime = DateTime.Now.AddHours(-10),
                    Message = "existing message"
                };

                _repository
                    .Setup(x => x.FindAll<MessageEntity>())
                    .Returns(new List<MessageEntity>{existingMessage});

                var result = _testObject.GetAll();

                Assert.That(result.Count, Is.EqualTo(1));
                Assert.That(result[0].Id, Is.EqualTo(existingMessage.Id));
                Assert.That(result[0].CreatedDateTime, Is.EqualTo(existingMessage.CreatedDateTime));
                Assert.That(result[0].Message, Is.EqualTo(existingMessage.Message));
            }

            [Test]
            public void Sorts_Results_By_CreatedDate()
            {
                var existingA = new MessageEntity { CreatedDateTime = DateTime.Now.AddHours(-10) };
                var existingB = new MessageEntity { CreatedDateTime = DateTime.Now.AddHours(-1) };
                
                _repository
                    .Setup(x => x.FindAll<MessageEntity>())
                    .Returns(new List<MessageEntity> { existingA, existingB });

                var result = _testObject.GetAll();

                Assert.That(result.Count, Is.EqualTo(2));
                Assert.That(result[0].Id, Is.EqualTo(existingB.Id));
                Assert.That(result[1].Id, Is.EqualTo(existingA.Id));
            }
        }
    }
}
