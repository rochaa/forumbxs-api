using System.Collections.Generic;
using System.Threading.Tasks;
using ForumBXS.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using Questions.Domain.Entities;
using Questions.Domain.Repositories;

namespace ForumBXS.Infra.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ForumBXSContext _context;

        public QuestionRepository(ForumBXSContext context)
        {
            _context = context;
        }

        public async Task Insert(Question question)
        {
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Question>> GetAll()
        {
            return await _context.Questions.ToListAsync();
        }
    }
}