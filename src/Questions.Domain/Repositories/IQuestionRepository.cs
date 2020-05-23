using System.Collections.Generic;
using System.Threading.Tasks;
using Questions.Domain.Entities;

namespace Questions.Domain.Repositories
{
    public interface IQuestionRepository
    {
        Task Insert(Question question);
        Task<IEnumerable<Question>> GetAll();
    }
}