using Api.Data.Context;
using Api.Data.Repositories;
using Api.Data.Repositories.Interfaces;

namespace Api.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        private IAccountRepository? _accountRepository;

        private ICategoryRepository? _categoryRepository;
        
        private ITransactionRepository? _transactionRepository;

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

        public ICategoryRepository CategoryRepository
        {
            get
            {
                if (_categoryRepository == null)
                    _categoryRepository = new CategoryRepository(_context);

                return _categoryRepository;
            }
        }
        
        public ITransactionRepository TransactionRepository
        {
            get
            {
                if (_transactionRepository == null)
                    _transactionRepository = new TransactionRepository(_context);

                return _transactionRepository;
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