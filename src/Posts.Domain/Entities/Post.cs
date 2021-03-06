using System;
using ForumBXS.Shared.Entities;

namespace Posts.Domain.Entities
{
    public abstract class Post : Entity
    {
        public Post(string text, string user)
        {
            Text = text;
            User = user;
            CreationDate = DateTime.UtcNow.BR();
            Likes = 0;
        }

        public string Text { get; private set; }
        public string User { get; private set; }
        public DateTime CreationDate { get; private set; }
        public int Likes { get; set; }

        public void Like()
        {
            Likes++;
        }
    }
}