using Flunt.Validations;
using ForumBXS.Shared.Commands;

namespace Questions.Domain.Commands
{
    public class NewQuestionCommand : Command
    {
        public string Text { get; set; }
        public string User { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(Text, "Text", "Necessário informar o texto da pergunta.")
                .HasMinLen(Text, 10, "Text", "Minímo de 10 caracteres para a pergunta.")
                .HasMaxLen(Text, 200, "Text", "Maximo de 200 caracteres para a pergunta.")
                .IsNotNullOrEmpty(User, "User", "Necessário informar o usuário.")
                .HasMinLen(User, 3, "User", "Minímo de 3 caracteres para o nome do usuário.")
                .HasMaxLen(User, 50, "User", "Maximo de 50 caracteres para o nome do usuário.")
            );
        }
    }
}