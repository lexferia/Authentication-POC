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
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> Get()
        {
            try
            {
                var result = (await _userRepository.GetItems())
                                .AsEnumerable()
                                .Select(item => _mapper.Map<UserDTO>(item));

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error encountered while fetching data. Message; {ex.Message}");
            }
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> Get(Guid id)
        {
            try
            {
                var result = await _userRepository.GetItem(id);
                if (result is null)
                    return NotFound();

                return Ok(_mapper.Map<UserDTO>(result));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error encountered while fetching data. Message; {ex.Message}");
            }
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AddUserDTO value)
        {
            try
            {
                var userEntity = _mapper.Map<User>(value);

                await _userRepository.AddItem(userEntity);

                var userDTO = _mapper.Map<UserDTO>(userEntity);

                return Created($"/api/users/{userDTO.Id}", userDTO);
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
        public async Task<ActionResult> Put(Guid id, [FromBody] EditUserDTO value)
        {
            try
            {
                var user = await _userRepository.GetItem(id);
                if (user is null)
                    return NotFound();

                user.Name = value.Name;
                user.Email = value.Email;
                user.Password = value.Password;
                user.Enabled = value.Enabled;
                user.RefreshToken = value.RefreshToken;
                user.RefreshTokenExpiryTime = value.RefreshTokenExpiryTime;
                user.RoleId = value.RoleId;

                await _userRepository.UpdateItem(user);

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
                var user = await _userRepository.GetItem(id);
                if (user is null)
                    return NotFound();

                await _userRepository.DeleteItem(id);

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
