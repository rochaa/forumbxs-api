using System;
using Flunt.Validations;
using ForumBXS.Shared.Commands;

namespace Posts.Domain.Commands
{
    public class LikeQuestionCommand : Command
    {
        public Guid QuestionId { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract()
                .IsTrue(QuestionId != null, "QuestionId", "Necessário informar o identificador da pergunta.")
            );
        }
    }
}