using AccountOwnerServer.CQRS.Commands.Owners;
using AccountOwnerServer.CQRS.Notifications;
using AccountOwnerServer.CQRS.Queries.Accounts;
using AccountOwnerServer.CQRS.Queries.Owners;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountOwnerServer.Controllers
{
    [Route("api/ownerAsyncCQRS")]
    [ApiController]
    public class OwnerAsyncCQRSController : ControllerBase
    {
        private readonly IMediator _mediator;
        private ILoggerManager _logger;
        private IMapper _mapper;

        public OwnerAsyncCQRSController(ILoggerManager logger, IMapper mapper, IMediator mediator)
        {
            _logger = logger;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetOwnersAsync()
        {
            try
            {
                var owners = await _mediator.Send(new GetOwnersQuery());
                _logger.LogInfo($"Returned all owners from database.");
                var ownersResult = _mapper.Map<IEnumerable<OwnerDto>>(owners);
                return Ok(ownersResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetOwnersAsync action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "OwnerByIdAsyncCQRS")]
        public async Task<IActionResult> GetOwnerByIdAsync(Guid id)
        {
            try
            {
                var getOwner = new GetOwnerByIdQuery { Id = id };
                var owner = await _mediator.Send(getOwner);
                if (owner == null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned owner with id: {id}");

                    var ownerResult = _mapper.Map<OwnerDto>(owner);
                    return Ok(ownerResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetOwnerByIdAsync action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}/account")]
        public async Task<IActionResult> GetOwnerWithDetailsAsync(Guid id)
        {
            try
            {
                var getOwner = new GetQwnerByIdWithDetailsQuery { Id = id };
                var owner = await _mediator.Send(getOwner);
                if (owner == null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned owner with details for id: {id}");

                    var ownerResult = _mapper.Map<OwnerDto>(owner);
                    return Ok(ownerResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetOwnerWithDetails action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateOwner([FromBody] OwnerForCreationDto owner)
        {
            var ownerEntity = _mapper.Map<Owner>(owner);

            if (owner == null)
            {
                _logger.LogError("Owner object sent from client is null.");
                return BadRequest("Owner object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid owner object sent from client.");
                return BadRequest("Invalid model object");
            }

            await _mediator.Send(new CreateOwnerCommand(ownerEntity));
            
            var createdOwner = _mapper.Map<OwnerDto>(ownerEntity);
            
            await _mediator.Publish(new OwnerAddedNotification(createdOwner));

            return CreatedAtRoute("OwnerById", new { id = createdOwner.Id }, createdOwner);     
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOwnerAsync(Guid id, [FromBody] OwnerForUpdateDto owner)
        {
            try
            {     
                if (owner == null)
                {
                    _logger.LogError("Owner object sent from client is null.");
                    return BadRequest("Owner object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid owner object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var getOwner = new GetOwnerByIdQuery { Id = id };
                var ownerEntity= await _mediator.Send(getOwner);
                if (ownerEntity == null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(owner, ownerEntity);

                await _mediator.Send(new UpdateOwnerCommand(ownerEntity));

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOwnerAsync(Guid id)
        {
            try
            {
                var getOwner = new GetOwnerByIdQuery { Id = id };
                var owner = await _mediator.Send(getOwner);
                if (owner == null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                var getAccounts = new GetAccountsByOwnerQuery { Id = id };
                var accounts = await _mediator.Send(getAccounts);
                if (accounts.Any())
                {
                    _logger.LogError($"Cannot delete owner with id: {id}. It has related accounts. Delete those accounts first");
                    return BadRequest("Cannot delete owner. It has related accounts. Delete those accounts first");
                }

                await _mediator.Send(new DeleteOwnerCommand(owner));

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}

