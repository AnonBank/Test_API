using ExamApi.Data;
using ExamApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamApi.Repository
{
    public interface IQuestionResultRepository
    {
        Task<ResponseSubmit> SubmitExam(RequestSubmit model);
        Task<ListQuestionResult> ListQuestionResult(RequestListQuestionResult model);
    }

    public class QuestionResultRepository : IQuestionResultRepository
    {
        private readonly AppDbContext _context;

        public QuestionResultRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ListQuestionResult> ListQuestionResult(RequestListQuestionResult input)
        {
            try
            {
                var query = await _context.QuestionResult
                    .AsNoTracking()
                    .OrderBy(o => o.question_no)
                    .Select(w => new ListQuestionResult.QuestionDetail
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

                return new ListQuestionResult
                {
                    Details = query
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResponseSubmit> SubmitExam(RequestSubmit input)
        {
            try
            {
                var questions = await _context.QuestionResult
                    .AsNoTracking()
                    .ToListAsync();

                int correct = 0;
                int wrong = 0;

                foreach (var answer in input.Answers)
                {
                    var question = questions
                        .FirstOrDefault(x => x.id == answer.QuestionId);

                    if (question == null)
                        continue;

                    if (question.crrect_answer == answer.SelectedAnswer)
                        correct++;
                    else
                        wrong++;
                }

                int score = correct;

                var examResult = new ExamResultModel
                {
                    user_name = input.UserName,
                    total_score = score,
                    total_correct = correct,
                    total_wrong = wrong,
                    created_date = DateTime.Now
                };

                await _context.ExamResult.AddAsync(examResult);

                await _context.SaveChangesAsync();

                return new ResponseSubmit
                {
                    UserName = input.UserName,
                    TotalScore = score,
                    TotalCorrect = correct,
                    TotalWrong = wrong
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}