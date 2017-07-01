using System.Linq;

namespace Tests.TestDoubles
{
    public class CustomerRepository
    {
        private readonly ICustomerDataTableGateway _customerDataGateway;

        public CustomerRepository(
            ICustomerDataTableGateway customerDataGateway)
        {
            _customerDataGateway = customerDataGateway;
        }

        public Customer Save(Customer customer)
        {
            if (customer.IsNew)
            {
                var customerDto = new CustomerDto
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName
                };

                var customerId = _customerDataGateway.Insert(customerDto);

                customer.Id = customerId;
            }
            else
            {
                var customerDto = new CustomerDto
                {
                    Id = customer.Id.GetValueOrDefault(0),
                    FirstName = customer.FirstName,
                    LastName = customer.LastName
                };

                _customerDataGateway.Update(customerDto);
            }

            return customer;
        }

        public Customer Get(int customerId)
        {
            var customerDto = _customerDataGateway.Get(customerId);

            if (customerDto == null)
                return null;

            return new Customer
            {
                Id = customerDto.Id,
                FirstName = customerDto.FirstName,
                LastName = customerDto.LastName
            };
        }

        public Customer[] GetAll()
        {
            var customerDtos = _customerDataGateway.GetAll();

            return customerDtos
                .Select(d => new Customer
                {
                    Id = d.Id,
                    FirstName = d.FirstName,
                    LastName = d.LastName
                })
                .ToArray();
        }
    }

    public class Customer
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool IsNew
        {
            get { return !Id.HasValue; }
        }

        public string GetDisplayName()
        {
            return string.Format("{0}, {1}", LastName, FirstName);
        }

        public int GetIdOrZero()
        {
            return Id.GetValueOrDefault(0);
        }
    }

    // Data Access

    public interface ICustomerDataTableGateway
    {
        int Insert(CustomerDto dto);
        void Update(CustomerDto dto);
        CustomerDto Get(int customerId);
        CustomerDto[] GetAll();
    }

    public class CustomerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}