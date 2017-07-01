using Moq;
using Xunit;

namespace Tests.TestDoubles
{
    public class CustomerDataTableGatewayDummy : ICustomerDataTableGateway
    {
        public int Insert(CustomerDto dto)
        {
            return 0;
        }

        public void Update(CustomerDto dto)
        {
        }

        public CustomerDto Get(int customerId)
        {
            return null;
        }

        public CustomerDto[] GetAll()
        {
            return new CustomerDto[0];
        }
    }

    public class CustomerRepositoryTests
    {
        [Fact]
        public void GetShouldReturnNullWhenGatewayReturnsNullGivenCustomerId()
        {
            var customerRepository = new CustomerRepository(new CustomerDataTableGatewayDummy());

            var customer = customerRepository.Get(10);

            Assert.Equal(customer, null);
        }

        [Fact]
        public void GetShouldReturnNullWhenGatewayReturnsNullGivenCustomerIdWithFramework()
        {
            var customerDataTableGatewayMock = new Mock<ICustomerDataTableGateway>();

            var customerRepository = new CustomerRepository(customerDataTableGatewayMock.Object);

            var customer = customerRepository.Get(10);

            Assert.Equal(customer, null);
        }
    }
}