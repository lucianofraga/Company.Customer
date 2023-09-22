using Company.Customer.API.Controllers;
using Company.Customer.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Moq;
using System.Net;

namespace Company.Customer.API.Tests
{
    public class CustomerControllerTests
    {
        private Mock<ICustomerService> _customerService;
        private Mock<LinkGenerator> _linkGenerator;
        private CustomerController _controller;

        public CustomerControllerTests()
        {
            _customerService = new Mock<ICustomerService>();
            _linkGenerator = new Mock<LinkGenerator>();
            _controller = new CustomerController(_customerService.Object, _linkGenerator.Object);
        }

        [Fact]
        public async Task AddCustomer_Success()
        {
            // Arrange
            Domain.Customer? customer = new Domain.Customer
            {
                FirstName = "Luciano",
                LastName = "Fraga2",
                Email = "lucianofraga2.web@gmail.com"
            };

            _customerService.Setup(p=> p.AddCustomer(It.IsAny<Domain.Customer>())).Returns(Task.FromResult<Domain.Customer?>(customer));

            // Act
            var result = await _controller.AddCustomer(customer).ConfigureAwait(false);

            // Assert
            (result as ObjectResult).StatusCode.Should().Be((int)HttpStatusCode.Created);
        }

        [Fact]
        public async Task AddCustomer_BadRequest()
        {
            // Arrange
            Domain.Customer? customer = new Domain.Customer
            {
                LastName = "Fraga2",
                Email = "lucianofraga2.web@gmail.com"
            };
            _controller.ModelState.AddModelError("FirstName", "FirstName is required.");

            // Act
            var result = await _controller.AddCustomer(customer).ConfigureAwait(false);

            // Assert
            (result as ObjectResult).StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task AddCustomer_InternalServerError()
        {
            // Arrange
            _customerService.Setup(p => p.AddCustomer(It.IsAny<Domain.Customer>())).Throws(new Exception("Server error"));

            // Act
            var result = await _controller.AddCustomer(new Domain.Customer()).ConfigureAwait(false);

            // Assert
            (result as StatusCodeResult).StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task GetCustomer_Success()
        {
            // Arrange
            Domain.Customer? customer = new Domain.Customer
            {
                Id = 1,
                FirstName = "Luciano",
                LastName = "Fraga2",
                Email = "lucianofraga2.web@gmail.com"
            };
            _customerService.Setup(p => p.GetCustomer(It.IsAny<int>())).Returns(Task.FromResult<Domain.Customer?>(customer));

            // Act
            var result = await _controller.GetCustomer(1).ConfigureAwait(false);

            // Assert
            (result as ObjectResult).StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetCustomer_BadRequest()
        {
            // Arrange           
            _controller.ModelState.AddModelError("Id", "Id must be greater than 0.");

            // Act
            var result = await _controller.GetCustomer(0).ConfigureAwait(false);

            // Assert
            (result as ObjectResult).StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetCustomer_InternalServerError()
        {
            // Arrange
            _customerService.Setup(p => p.GetCustomer(It.IsAny<int>())).Throws(new Exception("Server error"));

            // Act
            var result = await _controller.GetCustomer(1).ConfigureAwait(false);

            // Assert
            (result as StatusCodeResult).StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        }

    }
}
