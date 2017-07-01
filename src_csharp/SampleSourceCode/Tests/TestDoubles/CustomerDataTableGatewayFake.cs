using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests.TestDoubles
{
    public class CustomerDataTableGatewayFake : ICustomerDataTableGateway
    {
        private readonly List<CustomerDto> _customerDtos;

        private int _nextCustomerId;

        public CustomerDataTableGatewayFake()
        {
            _customerDtos = new List<CustomerDto>();
            _nextCustomerId = 1;
        }

        public int Insert(CustomerDto dto)
        {
            _customerDtos.Add(dto);

            var newCustomerId = _nextCustomerId++;

            dto.Id = newCustomerId;

            return newCustomerId;
        }

        public void Update(CustomerDto dto)
        {
            var savedDto = _customerDtos.FirstOrDefault(d => d.Id == dto.Id);

            if (savedDto == null)
                return;

            savedDto.FirstName = dto.FirstName;
            savedDto.LastName = dto.LastName;
        }

        public CustomerDto Get(int customerId)
        {
            return _customerDtos
                .FirstOrDefault(d => d.Id == customerId);
        }

        public CustomerDto[] GetAll()
        {
            return _customerDtos.ToArray();
        }

        public class CustomerRepositoryTests4
        {
            [Fact]
            public void GetAllShouldReturnTheExpectedCustomerSet()
            {
                var customerDataTableGatewayFake = new CustomerDataTableGatewayFake();

                customerDataTableGatewayFake.Insert(new CustomerDto()
                {
                    Id = 100,
                    FirstName = "firstname0",
                    LastName = "lastname0"
                });

                customerDataTableGatewayFake.Insert(new CustomerDto()
                {
                    Id = 101,
                    FirstName = "firstname1",
                    LastName = "lastname1"
                });

                customerDataTableGatewayFake.Insert(new CustomerDto()
                {
                    Id = 102,
                    FirstName = "firstname2",
                    LastName = "lastname2"
                });

                var customerRepository = new CustomerRepository(customerDataTableGatewayFake);

                var customers = customerRepository.GetAll();

                Assert.Equal(3, customers.Length);

                Assert.Equal(100, customers[0].GetIdOrZero());
                Assert.Equal("firstname0", customers[0].FirstName);
                Assert.Equal("lastname0", customers[0].LastName);

                Assert.Equal(101, customers[1].GetIdOrZero());
                Assert.Equal("firstname1", customers[1].FirstName);
                Assert.Equal("lastname1", customers[1].LastName);

                Assert.Equal(102, customers[2].GetIdOrZero());
                Assert.Equal("firstname2", customers[2].FirstName);
                Assert.Equal("lastname2", customers[2].LastName);
            }
        }
    }
}