using Company.Customer.Domain.Repositories;
using Persistence = Company.Customer.Persistence;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Company.Customer.Persistence.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private CustomerDbContext _dbContext;
        private IMapper _mapper;

        public CustomerRepository(CustomerDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task<Domain.Customer?> AddCustomer(Domain.Customer customer)
        {
            var created = _mapper.Map<Entities.Customer>(customer);
            
            var result = await _dbContext.Customers.AddAsync(created).ConfigureAwait(false);

            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            if (result != null)
            {
                return _mapper.Map<Domain.Customer>(created);
            }

            return null;
        }

        public async Task<Domain.Customer?> GetCustomer(int id)
        {
            var result = await _dbContext.Customers.Where(p => p.Id == id).FirstOrDefaultAsync().ConfigureAwait(false);
            
            if (result is null) return null;

            return _mapper.Map<Domain.Customer>(result);
        }

        public async Task<IEnumerable<Domain.Customer>?> GetCustomers()
        {
            var result = await _dbContext.Customers.ToListAsync().ConfigureAwait(false);

            if (result is null) return null;

            return _mapper.Map<IEnumerable<Domain.Customer>>(result);
        }
    }
}
