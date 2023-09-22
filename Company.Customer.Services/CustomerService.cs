using Company.Customer.Domain.Repositories;

namespace Company.Customer.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<Domain.Customer>?> GetCustomers();
        Task<Domain.Customer?> GetCustomer(int id);
        Task<Domain.Customer?> AddCustomer(Domain.Customer customer);
    }

    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repo;

        public CustomerService(ICustomerRepository repo)
        {
            _repo = repo;
        }

        public async Task<Domain.Customer?> GetCustomer(int id)
        {
            return await _repo.GetCustomer(id).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Domain.Customer>?> GetCustomers()
        {
            return await _repo.GetCustomers().ConfigureAwait(false);
        }

        public async Task<Domain.Customer?> AddCustomer(Domain.Customer customer)
        {
            return await _repo.AddCustomer(customer).ConfigureAwait(false);
        }
    }
}
