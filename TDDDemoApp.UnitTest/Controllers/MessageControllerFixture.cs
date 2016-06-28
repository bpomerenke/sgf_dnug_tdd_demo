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
                var input = new MessageController.MessageView{Message = "foo"};

                _testObject.SaveMessage(input);

                _repository.Verify(x=>x.Insert(It.Is<MessageEntity>(msg=>msg.Message == input.Message)));
                _repository.Verify(x=>x.SaveChanges(), Times.Once);
            }

            [Test]
            public void Returns_MessageView_With_Id_And_CreatedDate()
            {
                const int expectedId = 15;
                var expectedCreatedDate = DateTime.Now.AddHours(-1);
                var input = new MessageController.MessageView {Message = "bar"};

                _repository
                    .Setup(x => x.Insert(It.IsAny<MessageEntity>()))
                    .Callback<MessageEntity>(x =>
                    {
                        x.Id = expectedId;
                        x.CreatedDateTime = expectedCreatedDate;
                    });

                var result = _testObject.SaveMessage(input);

                Assert.That(result.Id, Is.EqualTo(expectedId));
                Assert.That(result.CreatedDateTime, Is.EqualTo(expectedCreatedDate));
                Assert.That(result.Message, Is.EqualTo(input.Message));

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
