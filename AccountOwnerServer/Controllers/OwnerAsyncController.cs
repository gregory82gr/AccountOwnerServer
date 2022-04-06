using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace AccountOwnerServer.Controllers
{
    [Route("api/ownerasync")]
    [ApiController]
    public class OwnerAsyncController : Controller
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        public OwnerAsyncController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOwners()
        {
            try
            {
                var owners = await _repository.Owner.GetAllOwnersAsync();
                _logger.LogInfo($"Returned all owners from database asynchronously.");
                var ownersResult = _mapper.Map<IEnumerable<OwnerDto>>(owners);
                return Ok(ownersResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllOwners action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
