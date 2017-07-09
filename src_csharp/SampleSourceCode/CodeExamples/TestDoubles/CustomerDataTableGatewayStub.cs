using Moq;
using Xunit;

namespace CodeExamples.TestDoubles
{
    public class CustomerDataTableGatewayStub : ICustomerDataTableGateway
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

        // **

        public bool GetAllWasCalled { get; private set; }

        public CustomerDto[] GetAll()
        {
            GetAllWasCalled = true;

            var customerDto0 = new CustomerDto()
            {
                Id = 100,
                FirstName = "firstname0",
                LastName = "lastname0"
            };

            var customerDto1 = new CustomerDto()
            {
                Id = 101,
                FirstName = "firstname1",
                LastName = "lastname1"
            };

            var customerDto2 = new CustomerDto()
            {
                Id = 102,
                FirstName = "firstname2",
                LastName = "lastname2"
            };

            return new[]
            {
                customerDto0,
                customerDto1,
                customerDto2
            };
        }
    }

    public class CustomerRepositoryTests3
    {
        [Fact]
        public void GetAllShouldReturnTheExpectedCustomerSet()
        {
            var customerDataTableGatewayStub = new CustomerDataTableGatewayStub();

            var customerRepository = new CustomerRepository(customerDataTableGatewayStub);

            var customers = customerRepository.GetAll();

            Assert.True(customerDataTableGatewayStub.GetAllWasCalled);

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

        [Fact]
        public void GetAllShouldReturnTheExpectedCustomerSetWithFramework()
        {
            var customerDataTableGatewayMock = new Mock<ICustomerDataTableGateway>();

            var customerDto0 = new CustomerDto()
            {
                Id = 100,
                FirstName = "firstname0",
                LastName = "lastname0"
            };

            var customerDto1 = new CustomerDto()
            {
                Id = 101,
                FirstName = "firstname1",
                LastName = "lastname1"
            };

            var customerDto2 = new CustomerDto()
            {
                Id = 102,
                FirstName = "firstname2",
                LastName = "lastname2"
            };

            var customerDtos = new[]
            {
                customerDto0,
                customerDto1,
                customerDto2
            };

            customerDataTableGatewayMock
                .Setup(g => g.GetAll())
                .Returns(customerDtos);

            var customerRepository = new CustomerRepository(customerDataTableGatewayMock.Object);

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