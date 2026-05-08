using ExamApi.Data;
using Microsoft.EntityFrameworkCore;

namespace ExamApi.Repository
{
    public interface IQueueRepository
    {
        Task<string> GetNextQueue();
        Task<string> ResetQueue();

        Task<string> CheckQueue();
    }

    public class QueueRepository : IQueueRepository
    {
        private readonly AppDbContext _context;

        public QueueRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> GetNextQueue()
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var data = await _context.QueueRunning
                    .FromSqlRaw("SELECT * FROM QueueRunning WITH (UPDLOCK, ROWLOCK) WHERE id = 1")
                    .FirstOrDefaultAsync() ?? throw new Exception("QueueRunning not found");

                char currentChar = string.IsNullOrEmpty(data.current_char)
                    ? 'A'
                    : data.current_char[0];

                int currentNumber = data.current_number;

                currentNumber++;

                if (currentNumber > 9)
                {
                    currentNumber = 0;

                    currentChar = currentChar == 'Z'
                        ? 'A'
                        : (char)(currentChar + 1);
                }

                data.current_char = currentChar.ToString();
                data.current_number = currentNumber;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return $"{currentChar}{currentNumber}";
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<string> CheckQueue()
        {
            try
            {
                var data = await _context.QueueRunning
                    .FirstOrDefaultAsync(x => x.id == 1);

                if (data == null)
                    return "No Queue";

                return $"{data.current_char}{data.current_number}";
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> ResetQueue()
        {
            var data = await _context.QueueRunning.FirstAsync();

            data.current_char = "A";
            data.current_number = 0;

            await _context.SaveChangesAsync();

            return "00";
        }
    }
}