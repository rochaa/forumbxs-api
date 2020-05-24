using Flunt.Validations;
using ForumBXS.Shared;
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
                .IsNotNullOrEmpty(Text, "Text", Message.PostTextIsNull)
                .HasMinLen(Text, 10, "Text", Message.PostTextMinChar)
                .HasMaxLen(Text, 500, "Text", Message.PostTextMaxChar)
                .IsNotNullOrEmpty(User, "User", Message.PostUserIsNull)
                .HasMinLen(User, 3, "User", Message.PostUserMinChar)
                .HasMaxLen(User, 50, "User", Message.PostUserMaxChar)
            );
        }
    }
}