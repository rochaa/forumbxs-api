using System.Threading.Tasks;
using Flunt.Notifications;
using System.Threading;
using ForumBXS.Shared.Handlers;
using Posts.Domain.Repositories;
using Posts.Domain.Commands;
using ForumBXS.Shared.Commands;
using Posts.Domain.Entities;
using ForumBXS.Shared;

namespace Posts.Domain.Handlers
{
    public class PostHandler : Notifiable,
        IHandler<NewQuestionCommand>,
        IHandler<NewAnswerCommand>,
        IHandler<LikeQuestionCommand>,
        IHandler<LikeAnswerCommand>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IAnswerRepository _answerRepository;

        public PostHandler(
            IQuestionRepository questionRepository,
            IAnswerRepository answerRepository)
        {
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
        }

        public async Task<CommandResult> Handle(NewQuestionCommand command, CancellationToken ct = default(CancellationToken))
        {
            // Validar comando (Fail Fast Validation)
            command.Validate();
            if (command.Invalid)
                return new CommandResult(Message.NewQuestionInvalidCommand, command.Notifications);

            // Criação da pergunta
            var question = new Question(command.Text, command.User);
            await _questionRepository.Insert(question);

            // Retornar o resultado
            return new CommandResult(Message.NewQuestionInsertedSucess, question);
        }

        public async Task<CommandResult> Handle(NewAnswerCommand command, CancellationToken ct = default(CancellationToken))
        {
            // Validar comando (Fail Fast Validation)
            command.Validate();
            if (command.Invalid)
                return new CommandResult(Message.NewAnswerInvalidCommand, command.Notifications);

            // Verifica se a pergunta existe
            var question = await _questionRepository.GetById(command.QuestionId.Value);
            if (question == null)
                return new CommandResult(Message.QuestionNotFound);

            // Criação da resposta
            var answer = new Answer(command.Text, command.User, command.QuestionId.Value);
            await _answerRepository.Insert(answer);

            // Retornar o resultado
            return new CommandResult(Message.NewAnswerInsertedSucess, answer);
        }

        public async Task<CommandResult> Handle(LikeQuestionCommand command, CancellationToken ct = default(CancellationToken))
        {
            // Validar comando (Fail Fast Validation)
            command.Validate();
            if (command.Invalid)
                return new CommandResult(Message.LikeInvalidCommand, command.Notifications);

            // Verifica se a pergunta existe
            var question = await _questionRepository.GetById(command.QuestionId);
            if (question == null)
                return new CommandResult(Message.QuestionNotFound);

            // Curtiu a pergunta
            question.Like();
            await _questionRepository.Update(question);

            // Retornar o resultado
            return new CommandResult(Message.LikedSucess, question);
        }

        public async Task<CommandResult> Handle(LikeAnswerCommand command, CancellationToken ct = default(CancellationToken))
        {
            // Validar comando (Fail Fast Validation)
            command.Validate();
            if (command.Invalid)
                return new CommandResult(Message.LikeInvalidCommand, command.Notifications);

            // Verifica se a resposta existe
            var answer = await _answerRepository.GetById(command.AnswerId);
            if (answer == null)
                return new CommandResult(Message.AnswerNotFound);

            // Curtiu a resposta
            answer.Like();
            await _answerRepository.Update(answer);

            // Retornar o resultado
            return new CommandResult(Message.LikedSucess, answer);
        }
    }
}