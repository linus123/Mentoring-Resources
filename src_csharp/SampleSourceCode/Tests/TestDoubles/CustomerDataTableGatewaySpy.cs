using Moq;
using Xunit;

namespace Tests.TestDoubles
{
    public class CustomerDataTableGatewaySpy : ICustomerDataTableGateway
    {
        private CustomerDto _updatedCustomerDto;

        public bool WasUpdateCalled { get; set; }

        public CustomerDto GetUpdatedCustomerDto()
        {
            return _updatedCustomerDto;
        }

        public void Update(CustomerDto dto)
        {
            WasUpdateCalled = true;
            _updatedCustomerDto = dto;
        }

        // **

        public bool WasInsertCalled { get; set; }

        public int Insert(CustomerDto dto)
        {
            WasInsertCalled = true;
            return 0;
        }

        // **

        public CustomerDto Get(int customerId)
        {
            return null;
        }

        // **

        public CustomerDto[] GetAll()
        {
            return new CustomerDto[0];
        }
    }

    public class CustomerRepositoryTests2
    {
        [Fact]
        public void SaveShouldCallUpdatedOnCustomerWithId()
        {
            var customerDataTableGatewaySpy = new CustomerDataTableGatewaySpy();

            var customer = new Customer()
            {
                Id = 100,
                FirstName = "firstname",
                LastName = "lastname"
            };

            var customerRepository = new CustomerRepository(customerDataTableGatewaySpy);

            customer = customerRepository.Save(customer);

            Assert.True(customerDataTableGatewaySpy.WasUpdateCalled);

            var customerDto = customerDataTableGatewaySpy.GetUpdatedCustomerDto();

            Assert.Equal(customerDto.Id, 100);
            Assert.Equal(customerDto.FirstName, "firstname");
            Assert.Equal(customerDto.LastName, "lastname");

            Assert.False(customerDataTableGatewaySpy.WasInsertCalled);
        }

        [Fact]
        public void SaveShouldCallUpdatedOnCustomerWithIdWithFrameWork()
        {
            var customerDataTableGatewayMock = new Mock<ICustomerDataTableGateway>();

            var customerRepository = new CustomerRepository(customerDataTableGatewayMock.Object);

            var customer = new Customer()
            {
                Id = 100,
                FirstName = "firstname",
                LastName = "lastname"
            };

            customer = customerRepository.Save(customer);

            customerDataTableGatewayMock.Verify(g => g.Update(
                It.Is<CustomerDto>(d =>
                    d.Id == 100
                    && d.FirstName == "firstname"
                    && d.LastName == "lastname")));
        }

    }
}