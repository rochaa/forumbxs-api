using Flunt.Validations;
using ForumBXS.Shared.Commands;

namespace Posts.Domain.Commands
{
    public abstract class PostCommand : Command
    {
        public string Text { get; set; }
        public string User { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(Text, "Text", "Necessário informar o texto.")
                .HasMinLen(Text, 10, "Text", "Minímo de 10 caracteres para o texto.")
                .HasMaxLen(Text, 500, "Text", "Maximo de 500 caracteres para o texto.")
                .IsNotNullOrEmpty(User, "User", "Necessário informar o usuário.")
                .HasMinLen(User, 3, "User", "Minímo de 3 caracteres para o nome do usuário.")
                .HasMaxLen(User, 50, "User", "Maximo de 50 caracteres para o nome do usuário.")
            );
        }
    }
}