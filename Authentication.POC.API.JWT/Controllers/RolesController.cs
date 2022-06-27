using Authentication.POC.API.JWT.Contracts;
using Authentication.POC.API.JWT.Data.Entities;
using Authentication.POC.Web.Shared.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Authentication.POC.API.JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RolesController(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDTO>>> Get()
        {
            try
            {
                var result = (await _roleRepository.GetItems())
                                .AsEnumerable()
                                .Select(item => _mapper.Map<RoleDTO>(item));

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error encountered while fetching data. Message; {ex.Message}");
            }
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> Get(Guid id)
        {
            try
            {
                var result = await _roleRepository.GetItem(id);
                if (result is null)
                    return NotFound();

                return Ok(_mapper.Map<RoleDTO>(result));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error encountered while fetching data. Message; {ex.Message}");
            }
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AddorEditRoleDTO value)
        {
            try
            {
                var roleEntity = _mapper.Map<Role>(value);

                await _roleRepository.AddItem(roleEntity);

                var roleDTO = _mapper.Map<RoleDTO>(roleEntity);

                return Created($"/api/roles/{roleDTO.Id}", roleDTO);
            }
            catch (InvalidDataException)
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error encountered while posting data. Message; {ex.Message}");
            }
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] AddorEditRoleDTO value)
        {
            try
            {
                var role = await _roleRepository.GetItem(id);
                if (role is null)
                    return NotFound();

                role.Name = value.Name;

                await _roleRepository.UpdateItem(role);

                return NoContent();
            }
            catch (InvalidDataException)
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error encountered while putting data. Message; {ex.Message}");
            }
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                var role = await _roleRepository.GetItem(id);
                if (role is null)
                    return NotFound();

                await _roleRepository.DeleteItem(id);

                return NoContent();
            }
            catch (InvalidDataException)
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error encountered while deleting data. Message; {ex.Message}");
            }
        }
    }
}
