using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumBXS.Shared;
using Moq;
using Posts.Domain.Commands;
using Posts.Domain.Entities;
using Posts.Domain.Handlers;
using Posts.Domain.Repositories;
using Xunit;

namespace ForumBXS.DomainTest.Commands
{
    public class NewQuestionTest
    {
        private readonly Mock<IQuestionRepository> _questionRepository = new Mock<IQuestionRepository>();
        private readonly PostHandler _handler;
        private NewQuestionCommand _validCommand;

        public NewQuestionTest()
        {
            _handler = new PostHandler(_questionRepository.Object, null);
            _validCommand = new NewQuestionCommand { Text = "Tecnologias para 2021?", User = "Admin" };
        }

        [Fact]
        public async Task Notify_When_Text_Invalid()
        {
            var invalidCommand = new NewQuestionCommand { Text = "Iae?", User = "Admin" };

            var result = await _handler.Handle(invalidCommand);
            var notifications = (List<string>)result.Data;
            var notificationWithTextMinChar = notifications.Where(n => n == Message.PostTextMinChar).Any();

            Assert.True(notificationWithTextMinChar);
        }

        [Fact]
        public async Task Notify_When_User_Invalid()
        {
            var invalidCommand = new NewQuestionCommand { Text = "Tecnologias para 2021?", User = "Bu" };

            var result = await _handler.Handle(invalidCommand);
            var notifications = (List<string>)result.Data;
            var notificationWithUserMinChar = notifications.Where(n => n == Message.PostUserMinChar).Any();

            Assert.True(notificationWithUserMinChar);
        }

        [Fact]
        public async Task Ensure_Data_Is_Sent_To_Repository()
        {
            var result = await _handler.Handle(_validCommand);

            _questionRepository.Verify(r => r.Insert(It.Is<Question>(q => q.Text == _validCommand.Text)));
        }

        [Fact]
        public async Task Notify_When_Question_Inserted_Success()
        {
            var result = await _handler.Handle(_validCommand);

            Assert.True(result.Sucess && result.Message == Message.NewQuestionInsertedSucess);
        }
    }
}