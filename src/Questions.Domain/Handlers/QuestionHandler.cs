using System.Threading.Tasks;
using Flunt.Notifications;
using System.Threading;
using ForumBXS.Shared.Handlers;
using Questions.Domain.Repositories;
using Questions.Domain.Commands;
using ForumBXS.Shared.Commands;
using Questions.Domain.Entities;

namespace Questions.Domain.Handlers
{
    public class QuestionHandler : Notifiable,
        IHandler<NewQuestionCommand>
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionHandler(
            IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
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
    }
}