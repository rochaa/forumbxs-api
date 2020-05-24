using Posts.Domain.Entities;

namespace ForumBXS.DomainTest.Builders
{
    public class QuestionBuilder
    {
        protected string Text;
        protected string User;

        public static QuestionBuilder Novo()
        {
            return new QuestionBuilder
            {
                Text = "Quando usar blockchain?",
                User = "Satoshi"
            };
        }

        public QuestionBuilder ComText(string text)
        {
            Text = text;
            return this;
        }

        public Question Build()
        {
            return new Question(Text, User);;
        }
    }
}