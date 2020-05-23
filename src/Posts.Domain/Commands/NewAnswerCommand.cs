using System;
using Flunt.Validations;

namespace Posts.Domain.Commands
{
    public class NewAnswerCommand : PostCommand
    {
        public Guid? QuestionId { get; set; }

        public override void Validate()
        {
            base.Validate();
  
            AddNotifications(new Contract()
                .IsTrue(QuestionId != null, "QuestionId", "Necessário informar o identificador da pergunta.")
            );
        }
    }
}