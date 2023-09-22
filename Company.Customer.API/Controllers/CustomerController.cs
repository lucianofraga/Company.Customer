using Company.Customer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Net;

namespace Company.Customer.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly LinkGenerator _linkGenerator;

        public CustomerController(ICustomerService customerService, LinkGenerator linkGenerator)
        {
            _customerService = customerService;
            _linkGenerator = linkGenerator;
        }

        /// <summary>
        /// Get the Customer by Id
        /// </summary>
        /// <param name="id">Customer Id</param>
        /// <returns>Customer</returns>        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            if (id < 1) return BadRequest(ModelState);

            try
            {
                var result = await _customerService.GetCustomer(id).ConfigureAwait(false);
                return result is not null ? Ok(result) : NoContent();
            }
            catch (Exception exc)
            {
                // TODO: Add error message to logging
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Get All the Customers
        /// </summary>
        /// <returns>List of Customers</returns>
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            try
            {
                var result = await _customerService.GetCustomers().ConfigureAwait(false);
                return result is not null ? Ok(result) : NoContent();
            }
            catch (Exception exc)
            {
                // TODO: Add error message to logging
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Add a new Customer
        /// </summary>
        /// <param name="payload">Customer's data</param>
        /// <returns>Created Customer</returns>
        [HttpPost ("[action]")]
        public async Task<ActionResult> AddCustomer([FromBody] Domain.Customer payload)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var created = await _customerService.AddCustomer(payload).ConfigureAwait(false);
                var uri = _linkGenerator.GetPathByAction("GetCustomer", "Customer", new { Id = created?.Id }) ?? "";

                return Created(uri, created);
            }
            catch (Exception exc)
            {
                // TODO: Add error message to logging
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
