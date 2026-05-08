using ExamApi.Data;
using ExamApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamApi.Repository
{
    public interface ITextRepository
    {
        Task<ListText> GetText(RequestText model);
    }

    public class TextRepository : ITextRepository
    {
        private readonly AppDbContext _context;

        public TextRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ListText> GetText(RequestText input)
        {
            try
            {
                var text = _context.Text
                    .AsNoTracking()
                    .Where(w => w.text_id == input.TextId)
                    .Select(w => new ListText.TextDetail
                    {
                        TextIndex = w.text_index,
                        TextId = w.text_id,
                        TextName = w.text_name,
                    })
                    .ToList();

                return new ListText
                {
                    Details = text
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}