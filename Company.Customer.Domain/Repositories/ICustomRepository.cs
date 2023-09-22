using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Customer.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer?> AddCustomer(Customer customer);
        Task<Domain.Customer?> GetCustomer(int id);

        Task<IEnumerable<Domain.Customer>?> GetCustomers();
    }
}
