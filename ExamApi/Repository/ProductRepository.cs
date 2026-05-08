using ExamApi.Data;
using ExamApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamApi.Repository
{
    public interface IProductRepository
    {
        Task SaveAsync(RequestCreateProduct model);
        Task DeleteAsync(RequestDeleteProduct model);
        Task<ListProduct> ListProductBarcode(RequestListProduct model);
    }

    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ListProduct> ListProductBarcode(RequestListProduct input)
        {
            try
            {
                var productBarcode = _context.ProductBarcode
                    .AsNoTracking()
                    .ToList();

                var query = productBarcode.Select(w => new ListProduct.ProductDetail
                {
                    Id = w.id,
                    BarcodeId = w.barcode_id,
                    Barcode = w.barcode,
                    CreatedDate = w.created_date
                }).OrderBy(o => o.CreatedDate).ToList();

                return await Task.FromResult(new ListProduct
                {
                    Details = query
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task SaveAsync(RequestCreateProduct input)
        {
            try
            {
                if (!BarcodeHelper.Validate(input.BarcodeId))
                    throw new Exception("Invalid Barcode Format");

                var base64 = BarcodeHelper.GenerateCode39(input.BarcodeId);

                var entity = new ProductBarcodeModel
                {
                    barcode_id = input.BarcodeId,
                    barcode = base64,
                    created_date = DateTime.Now
                };

                await _context.ProductBarcode.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteAsync(RequestDeleteProduct input)
        {
            try
            {
                var item = await _context.ProductBarcode
                    .FirstOrDefaultAsync(x => x.barcode_id == input.BarcodeId) ?? throw new Exception("Data not found");
                
                _context.ProductBarcode.Remove(item);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}