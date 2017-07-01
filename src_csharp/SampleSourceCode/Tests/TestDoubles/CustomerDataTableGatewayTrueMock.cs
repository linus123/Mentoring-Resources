using System.Collections.Generic;
using Xunit;

namespace Tests.TestDoubles
{
    public class CustomerDataTableGatewayTrueMock : ICustomerDataTableGateway
    {
        private readonly List<string> _eventStream;
        private readonly CustomerDataTableGatewayFake _customerDataTableGatewayFake;

        public CustomerDataTableGatewayTrueMock()
        {
            _customerDataTableGatewayFake = new CustomerDataTableGatewayFake();

            _eventStream = new List<string>();
        }

        public int Insert(CustomerDto dto)
        {
            var e = string.Format("Insert Called - {0}", GetSummary(dto));
            _eventStream.Add(e);

            return _customerDataTableGatewayFake.Insert(dto);
        }

        public void Update(CustomerDto dto)
        {
            var e = string.Format("Update Called - {0}", GetSummary(dto));
            _eventStream.Add(e);

            _customerDataTableGatewayFake.Update(dto);
        }

        public CustomerDto Get(int customerId)
        {
            var e = string.Format("Get Called - CustomerId: {0}", customerId);
            _eventStream.Add(e);

            return _customerDataTableGatewayFake.Get(customerId);
        }

        public CustomerDto[] GetAll()
        {
            var e = "Get All Called";
            _eventStream.Add(e);

            return _customerDataTableGatewayFake.GetAll();
        }

        public static string GetSummary(
            CustomerDto dto)
        {
            return string.Format(
                "'{0} - {1}, {2}'",
                dto.Id,
                dto.LastName,
                dto.FirstName);
        }

        public string[] GetAllEvents()
        {
            return _eventStream.ToArray();
        }
    }

    public class CustomerRepositoryTests5
    {
        [Fact]
        public void ShouldReturnTheExpectedCalls()
        {
            var customerDataTableGatewayMock = new CustomerDataTableGatewayTrueMock();

            var customerRepository = new CustomerRepository(customerDataTableGatewayMock);

            var customer = new Customer()
            {
                FirstName = "John",
                LastName = "Doe"
            };

            customer = customerRepository.Save(customer);

            customer.FirstName = "Bob";

            customer = customerRepository.Save(customer);

            var savedCustomer = customerRepository.Get(customer.GetIdOrZero());

            var events = customerDataTableGatewayMock.GetAllEvents();

            Assert.Equal("Insert Called - '0 - Doe, John'", events[0]);
            Assert.Equal("Update Called - '1 - Doe, Bob'", events[1]);
            Assert.Equal("Get Called - CustomerId: " + savedCustomer.GetIdOrZero(), events[2]);
        }
    }
}