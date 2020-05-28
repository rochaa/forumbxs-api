using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Posts.Domain.Entities;

namespace Posts.Domain.Repositories
{
    public interface IAnswerRepository
    {
        Task Insert(Answer answer);
        Task Update(Answer answer);
        Task<IEnumerable<Answer>> GetByQuestion(Guid questionId);
        Task<Answer> GetById(Guid id);
    }
}