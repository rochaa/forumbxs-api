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
    public class AnswerRepository : IAnswerRepository
    {
        private readonly ForumBXSContext _context;

        public AnswerRepository(ForumBXSContext context)
        {
            _context = context;
        }

        public async Task Insert(Answer answer)
        {
            _context.Answers.Add(answer);
            await _context.SaveChangesAsync();
        }

        public async Task<Answer> GetById(Guid id)
        {
            return await _context.Answers
                .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<IEnumerable<Answer>> GetByQuestion(Guid questionId)
        {
            return await _context.Answers
                .Where(a => a.QuestionId == questionId)
                .ToListAsync();
        }
    }
}