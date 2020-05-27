using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumBXS.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using Posts.Domain.Entities;
using Posts.Domain.Repositories;

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
            return await _context.Questions
                .Include(a => a.Answers)
                .OrderByDescending(d => d.CreationDate)
                .ToListAsync();
        }

        public async Task<Question> GetById(Guid id)
        {
            return await _context.Questions
                .FirstOrDefaultAsync(q => q.Id == id);
        }
    }
}