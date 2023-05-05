using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using FarzadsTask.Dto;
using FarzadsTask.Repositories;
using FarzadsTask.Domain;
using FarzadsTask.Validators;

namespace FarzadsTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CustomerDto>> GetByIdAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CustomerDto>(customer));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAllAsync()
        {
            var customers = await _customerRepository.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<CustomerDto>>(customers));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> AddAsync(CustomerDto customerDto)
        {
            var validator = new CustomerValidator();
            var validationResult = await validator.ValidateAsync(customerDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var customer = _mapper.Map<Customer>(customerDto);
            await _customerRepository.AddAsync(customer);

            return CreatedAtAction("AddAsync", new { id = customer.Id }, _mapper.Map<CustomerDto>(customer));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<CustomerDto>> UpdateAsync(int id, CustomerDto customerDto)
        {
            if (id != customerDto.Id)
            {
                return BadRequest();
            }

            var validator = new CustomerValidator();
            var validationResult = await validator.ValidateAsync(customerDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var customer = _mapper.Map<Customer>(customerDto);
            await _customerRepository.UpdateAsync(customer);

            return Ok(_mapper.Map<CustomerDto>(customer));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            await _customerRepository.DeleteAsync(customer);

            return NoContent();
        }
    }
}
