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
        private readonly ICityRepository _cityRepository;
        public CustomerController(ICustomerRepository customerRepository, IMapper mapper, ICityRepository cityRepository)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _cityRepository = cityRepository;
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

            var customerDtos = customers.Select(c =>
            {
                var customerDto = _mapper.Map<CustomerDto>(c);
                if (c.City != null)
                {
                    customerDto.CityName = c.City.Name;
                }
                return customerDto;
            });

            return Ok(_mapper.Map<IEnumerable<CustomerDto>>(customerDtos));
        }

        //[HttpPost]
        //public async Task<ActionResult<CustomerDto>> AddAsync(CustomerDto customerDto)
        //{
        //    var validator = new CustomerValidator();
        //    var validationResult = await validator.ValidateAsync(customerDto);

        //    if (!validationResult.IsValid)
        //    {
        //        return BadRequest(validationResult.Errors);
        //    }

        //    var customer = _mapper.Map<Customer>(customerDto);
        //    await _customerRepository.AddAsync(customer);

        //    return CreatedAtAction("AddAsync", new { id = customer.Id }, _mapper.Map<CustomerDto>(customer));
        //}


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
            if (customerDto.CityId.HasValue)
            {
                var city = await _cityRepository.GetByIdAsync(customerDto.CityId.Value);
                if (city != null)
                {
                    customer.City = city;
                }
            }
            await _customerRepository.AddAsync(customer);

            return CreatedAtAction("AddAsync", new { id = customer.Id }, _mapper.Map<CustomerDto>(customer));
        }



        //[HttpPut("{id:int}")]
        //public async Task<ActionResult<CustomerDto>> UpdateAsync(int id, CustomerDto customerDto)
        //{
        //    if (id != customerDto.Id)
        //    {
        //        return BadRequest();
        //    }

        //    var validator = new CustomerValidator();
        //    var validationResult = await validator.ValidateAsync(customerDto);

        //    if (!validationResult.IsValid)
        //    {
        //        return BadRequest(validationResult.Errors);
        //    }

        //    var customer = _mapper.Map<Customer>(customerDto);
        //    await _customerRepository.UpdateAsync(customer);

        //    return Ok(_mapper.Map<CustomerDto>(customer));
        //}


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

            var existingCustomer = await _customerRepository.GetByIdAsync(id);
            if (existingCustomer == null)
            {
                return NotFound();
            }

            existingCustomer.FirstName = customerDto.FirstName;
            existingCustomer.LastName = customerDto.LastName;
            existingCustomer.Email = customerDto.Email;
            existingCustomer.PhoneNumber = customerDto.PhoneNumber;
            existingCustomer.Address = customerDto.Address;

            if (customerDto.CityId.HasValue)
            {
                var city = await _cityRepository.GetByIdAsync(customerDto.CityId.Value);
                if (city != null)
                {
                    existingCustomer.City = city;
                }
            }
            else
            {
                existingCustomer.City = null;
            }

            await _customerRepository.UpdateAsync(existingCustomer);

            return Ok(_mapper.Map<CustomerDto>(existingCustomer));
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
