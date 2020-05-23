using System.Collections.Generic;

namespace Posts.Domain.Entities
{
    public class Question : Post
    {
        public Question(string text, string user) : base(text, user) { }

        public List<Answer> Answers { get; set; }
    }
}