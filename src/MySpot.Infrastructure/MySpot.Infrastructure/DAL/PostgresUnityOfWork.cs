namespace MySpot.Infrastructure.DAL
{
    internal class PostgresUnityOfWork : IUnityOfWork
    {
        private readonly MySpotDbContext _context;

        public PostgresUnityOfWork(MySpotDbContext context)
        {
            _context = context;
        }
       
        public async Task ExecuteAsync(Func<Task> action)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await action();
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
