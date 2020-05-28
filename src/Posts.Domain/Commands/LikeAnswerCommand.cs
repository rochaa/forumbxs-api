using System;
using Flunt.Validations;
using ForumBXS.Shared.Commands;

namespace Posts.Domain.Commands
{
    public class LikeAnswerCommand : Command
    {
        public Guid AnswerId { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract()
                .IsTrue(AnswerId != null, "AnswerId", "Necessário informar o identificador da resposta.")
            );
        }
    }
}