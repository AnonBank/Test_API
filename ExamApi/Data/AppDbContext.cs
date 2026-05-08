using Microsoft.EntityFrameworkCore;
using ExamApi.Models;

namespace ExamApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<AuthModel> Auth { get; set; }
        public DbSet<DocumentsModel> Documents { get; set; }
        public DbSet<ProfileModel> Profile { get; set; }
        public DbSet<TextModel> Text { get; set; }
        public DbSet<QueueModel> QueueRunning { get; set; }
        public DbSet<ProductBarcodeModel> ProductBarcode { get; set; }
        public DbSet<ProductQrModel> ProductQr { get; set; }
        public DbSet<QuestionModel> Question { get; set; }
        public DbSet<QuestionResultModel> QuestionResult { get; set; }
        public DbSet<ExamResultModel> ExamResult { get; set; }
    }
}