using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepository _repository;
        private readonly IMapper _mapper;

        public PlatformsController(IPlatformRepository repo, IMapper mapper)
        {
            _repository = repo;
            _mapper = mapper;
        }

        [HttpGet("getplatforms")]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            try
            {
                var platformItems = _repository.GetAllPlatforms();
                return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItems));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("getplatformbyid/{id}")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            try
            {
                var platformItem = _repository.GetPlatformById(id);
                if(platformItem == null)
                {
                    return BadRequest("item does not exist");
                }
                return Ok(_mapper.Map<PlatformReadDto>(platformItem));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("createplatform")]
        public ActionResult CreatePlatform(PlatformCreateDto platformCreateDto)
        {
            try
            {
                var platformModel = _mapper.Map<Platform>(platformCreateDto);
                _repository.CreatePlatform(platformModel);
                _repository.SaveChanges();

                return Created();

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
