using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumBXS.DomainTest.Builders;
using ForumBXS.Shared;
using Moq;
using Posts.Domain.Commands;
using Posts.Domain.Entities;
using Posts.Domain.Handlers;
using Posts.Domain.Repositories;
using Xunit;

namespace ForumBXS.DomainTest.Commands
{
    public class NewAnswerTest
    {

        private readonly Mock<IQuestionRepository> _questionRepository = new Mock<IQuestionRepository>();
        private readonly Mock<IAnswerRepository> _answerRepository = new Mock<IAnswerRepository>();
        private readonly PostHandler _handler;
        private NewAnswerCommand _validCommand;

        public NewAnswerTest()
        {
            _handler = new PostHandler(_questionRepository.Object, _answerRepository.Object);
            _validCommand = new NewAnswerCommand { Text = "Go tem crescido bastante.", User = "Admin", QuestionId = Guid.NewGuid() };
        }

        [Fact]
        public async Task Notify_When_Text_Invalid()
        {
            var invalidCommand = new NewAnswerCommand { Text = "Oi?", User = "Admin" };

            var result = await _handler.Handle(invalidCommand);
            var notifications = (List<string>)result.Data;
            var notificationWithTextMinChar = notifications.Where(n => n == Message.PostTextMinChar).Any();

            Assert.True(notificationWithTextMinChar);
        }

        [Fact]
        public async Task Notify_When_User_Invalid()
        {
            var invalidCommand = new NewAnswerCommand { Text = "Go tem crescido bastante.", User = "Bo" };

            var result = await _handler.Handle(invalidCommand);
            var notifications = (List<string>)result.Data;
            var notificationWithUserMinChar = notifications.Where(n => n == Message.PostUserMinChar).Any();

            Assert.True(notificationWithUserMinChar);
        }

        [Fact]
        public async Task Notify_When_Question_Not_Found()
        {
            Question questionNotFound = null;

            _questionRepository.Setup(o => o.GetById(_validCommand.QuestionId.Value)).ReturnsAsync(questionNotFound);
            var result = await _handler.Handle(_validCommand);

            Assert.True(result.Message == Message.QuestionNotFound);
        }

        [Fact]
        public async Task Ensure_Data_Is_Sent_To_Repository()
        {
            Question validQuestion = QuestionBuilder.Novo().Build();

            _questionRepository.Setup(o => o.GetById(_validCommand.QuestionId.Value)).ReturnsAsync(validQuestion);
            var result = await _handler.Handle(_validCommand);

            _answerRepository.Verify(r => r.Insert(It.Is<Answer>(q => q.Text == _validCommand.Text)));
        }

        [Fact]
        public async Task Notify_When_Answer_Inserted_Success()
        {
            Question validQuestion = QuestionBuilder.Novo().Build();

            _questionRepository.Setup(o => o.GetById(_validCommand.QuestionId.Value)).ReturnsAsync(validQuestion);
            var result = await _handler.Handle(_validCommand);

            Assert.True(result.Sucess && result.Message == Message.NewAnswerInsertedSucess);
        }
    }
}