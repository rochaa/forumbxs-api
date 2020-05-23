using System;

namespace Posts.Domain.Entities
{
    public class Answer : Post
    {
        public Answer(string text, string user, Guid questionId) : base(text, user)
        {
            QuestionId = questionId;
        }

        public Guid QuestionId { get; set; }
    }
}