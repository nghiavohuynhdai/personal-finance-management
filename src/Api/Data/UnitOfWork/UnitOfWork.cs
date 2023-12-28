using Api.Features.Account;
using Api.Repositories;

namespace Api.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        private IAccountRepository? _accountRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IAccountRepository AccountRepository
        {
            get
            {
                if (_accountRepository == null)
                    _accountRepository = new AccountRepository(_context);

                return _accountRepository;
            }
        }

        public void Commit()
            => _context.SaveChanges();


        public async Task CommitAsync(CancellationToken cancellationToken = default)
            => await _context.SaveChangesAsync();


        public void Rollback()
            => _context.Dispose();


        public async Task RollbackAsync(CancellationToken cancellationToken = default)
            => await _context.DisposeAsync();
    }
}