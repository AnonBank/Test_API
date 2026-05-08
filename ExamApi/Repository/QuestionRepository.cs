using ExamApi.Data;
using ExamApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamApi.Repository
{
    public interface IQuestionRepository
    {
        Task SaveAsync(RequestCreateQuestion model);

        Task DeleteAsync(RequestDeleteQuestion model);

        Task<ListQuestion> ListQuestion(RequestListQuestion model);
    }

    public class QuestionRepository : IQuestionRepository
    {
        private readonly AppDbContext _context;

        public QuestionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ListQuestion> ListQuestion(RequestListQuestion input)
        {
            try
            {
                var query = await _context.Question
                    .AsNoTracking()
                    .OrderBy(o => o.question_no)
                    .Select(w => new ListQuestion.QuestionDetail
                    {
                        Id = w.id,
                        QuestionNo = w.question_no,
                        QuestionText = w.question_text,
                        Answer1 = w.answer1,
                        Answer2 = w.answer2,
                        Answer3 = w.answer3,
                        Answer4 = w.answer4
                    })
                    .ToListAsync();

                return new ListQuestion
                {
                    Details = query
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task SaveAsync(RequestCreateQuestion input)
        {
            try
            {
                var nextNo = await _context.Question
                    .MaxAsync(x => (int?)x.question_no) ?? 0;

                nextNo++;

                var entity = new QuestionModel
                {
                    question_no = nextNo,
                    question_text = input.QuestionText,
                    answer1 = input.Answer1,
                    answer2 = input.Answer2,
                    answer3 = input.Answer3,
                    answer4 = input.Answer4,
                    created_date = DateTime.Now
                };

                await _context.Question.AddAsync(entity);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteAsync(RequestDeleteQuestion input)
        {
            try
            {
                var item = await _context.Question
                    .FirstOrDefaultAsync(x => x.id == input.Id)
                    ?? throw new Exception("Question not found");

                _context.Question.Remove(item);

                await _context.SaveChangesAsync();

                var questions = await _context.Question
                    .OrderBy(o => o.question_no)
                    .ToListAsync();

                int running = 1;

                foreach (var q in questions)
                {
                    q.question_no = running;
                    running++;
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}