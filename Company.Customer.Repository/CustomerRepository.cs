using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Customer.Repository
{
    public interface ICustomerRepository
    {
        Task<Domain.Customer?> GetCustomer(int id);

        Task<IEnumerable<Domain.Customer>?> GetCustomers();
    }

    public class CustomerRepository : ICustomerRepository
    {
        private IEnumerable<Domain.Customer> _customerList;

        public CustomerRepository()
        {
            _customerList = new List<Domain.Customer>
            {
                new Domain.Customer {Id = 1, FistName="Luciano", LastName="Fraga", Email="lucianofraga.web@gmail.com"},
                new Domain.Customer {Id = 2, FistName="Anelise", LastName="Fraga", Email="lucianofraga.web@gmail.com"},
                new Domain.Customer {Id = 3, FistName="Eduardo", LastName="Fraga", Email="lucianofraga.web@gmail.com"},
            };
        }

        public async Task<Domain.Customer?> GetCustomer(int id)
        {
            return await Task.FromResult<Domain.Customer?>(_customerList.FirstOrDefault());
        }

        public async Task<IEnumerable<Domain.Customer>?> GetCustomers()
        {
            return await Task.FromResult<IEnumerable<Domain.Customer>?>(_customerList.ToList());
        }
    }
}
