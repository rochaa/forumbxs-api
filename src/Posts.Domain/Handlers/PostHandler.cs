using System.Threading.Tasks;
using Flunt.Notifications;
using System.Threading;
using ForumBXS.Shared.Handlers;
using Posts.Domain.Repositories;
using Posts.Domain.Commands;
using ForumBXS.Shared.Commands;
using Posts.Domain.Entities;

namespace Posts.Domain.Handlers
{
    public class PostHandler : Notifiable,
        IHandler<NewQuestionCommand>,
        IHandler<NewAnswerCommand>
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
                return new CommandResult("Dados para criar a pergunta inválidos", command.Notifications);

            // Criação da pergunta
            var question = new Question(command.Text, command.User);
            await _questionRepository.Insert(question);

            // Retornar o resultado
            return new CommandResult("Pergunta criada com sucesso.", question);
        }

        public async Task<CommandResult> Handle(NewAnswerCommand command, CancellationToken ct = default(CancellationToken))
        {
            // Validar comando (Fail Fast Validation)
            command.Validate();
            if (command.Invalid)
                return new CommandResult("Dados para criar a resposta inválidos", command.Notifications);

            // Verifica se a pergunta existe
            var question = await _questionRepository.GetById(command.QuestionId.Value);
            if (question == null)
                return new CommandResult("Pergunta não encontrada.");

            // Criação da resposta
            var answer = new Answer(command.Text, command.User, command.QuestionId.Value);
            await _answerRepository.Insert(answer);

            // Retornar o resultado
            return new CommandResult("Resposta criada com sucesso.", answer);
        }
    }
}