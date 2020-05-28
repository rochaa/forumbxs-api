using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Posts.Domain.Entities;

namespace Posts.Domain.Repositories
{
    public interface IQuestionRepository
    {
        Task Insert(Question question);
        Task Update(Question question);
        Task<IEnumerable<Question>> GetAll();
        Task<Question> GetById(Guid id);
    }
}