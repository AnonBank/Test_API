using ExamApi.Data;
using ExamApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamApi.Repository
{
    public interface IProductQrRepository
    {
        Task SaveAsync(RequestCreateProductQr model);
        Task DeleteAsync(RequestDeleteProductQr model);
        Task<ListProductQr> ListProductQr(RequestListProductQr model);
    }

    public class ProductQrRepository : IProductQrRepository
    {
        private readonly AppDbContext _context;

        public ProductQrRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ListProductQr> ListProductQr(RequestListProductQr input)
        {
            try
            {
                var ProductQr = _context.ProductQr
                    .AsNoTracking()
                    .ToList();

                var query = ProductQr.Select(w => new ListProductQr.ProductQrDetail
                {
                    Id = w.id,
                    BarcodeId = w.barcode_id,
                    CreatedDate = w.created_date
                }).OrderBy(o => o.CreatedDate).ToList();

                return await Task.FromResult(new ListProductQr
                {
                    Details = query
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task SaveAsync(RequestCreateProductQr input)
        {
            try
            {
                //if (!BarcodeHelper.Validate(input.BarcodeId))
                //    throw new Exception("Invalid Barcode Format");

                //var base64 = BarcodeHelper.GenerateCode39(input.BarcodeId);

                var entity = new ProductQrModel
                {
                    barcode_id = input.BarcodeId,
                    barcode = "",
                    created_date = DateTime.Now
                };

                await _context.ProductQr.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteAsync(RequestDeleteProductQr input)
        {
            try
            {
                var item = await _context.ProductQr
                    .FirstOrDefaultAsync(x => x.barcode_id == input.BarcodeId) ?? throw new Exception("Data not found");
                
                _context.ProductQr.Remove(item);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}