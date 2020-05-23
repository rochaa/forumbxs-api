using System;
using ForumBXS.Shared.Entities;

namespace Questions.Domain.Entities
{
    public class Question : Entity
    {
        public Question(string text, string user)
        {
            Text = text;
            User = user;
            CreationDate = DateTime.UtcNow.BR();
        }

        public string Text { get; private set; }
        public string User { get; private set; }
        public DateTime CreationDate { get; private set; }
    }
}