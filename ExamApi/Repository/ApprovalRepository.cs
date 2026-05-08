using ExamApi.Data;
using ExamApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamApi.Repository
{
    public interface IApprovalRepository
    {
        Task<ListApproval> ListApproval(RequestListApproval model);
        Task Approve(RequestSaveApproval model);
        Task Reject(RequestSaveApproval model);
    }
    public class ApprovalRepository : IApprovalRepository
    {
        private readonly AppDbContext _context;

        public ApprovalRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ListApproval> ListApproval(RequestListApproval input)
        {
            try
            {
                var documents = _context.Documents
                    .AsNoTracking()
                    .ToList();

                var query = documents.Select(w => new ListApproval.ApprovalDetail
                {
                    DocumentIndex = w.Document_index,
                    DocumentId = w.Document_id,
                    Status = w.Status,
                    Remark = w.Remark,
                }).OrderBy(o => o.DocumentId).ToList();

                return await Task.FromResult(new ListApproval
                {
                    Details = query
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Approve(RequestSaveApproval input)
        {
            try
            {
                var lsitDocuments = input.Documents
                    .Select(w => w.DocumentIndex)
                    .ToList();

                var documents = await _context.Documents
                    .Where(w => lsitDocuments.Contains(w.Document_index))
                    .ToListAsync();

                documents.ForEach(w =>
                {
                    w.Status = 2;
                    w.Remark = input.Remark;
                });

                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Reject(RequestSaveApproval input)
        {
            try
            {
                var lsitDocuments = input.Documents
                    .Select(w => w.DocumentIndex)
                    .ToList();

                var documents = await _context.Documents
                    .Where(w => lsitDocuments.Contains(w.Document_index))
                    .ToListAsync();

                documents.ForEach(w =>
                {
                    w.Status = 3;
                    w.Remark = input.Remark;
                });

                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}