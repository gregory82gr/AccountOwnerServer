using AccountOwnerServer.Notifications;
using AccountOwnerServer.Owners.Commands;
using AccountOwnerServer.Owners.Queries;
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
        private IRepositoryWrapperAsync _repository;
        private IMapper _mapper;

        public OwnerAsyncCQRSController(ILoggerManager logger, IRepositoryWrapperAsync repository, IMapper mapper, IMediator mediator)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetOwnersAsync()
        {
            var owners = await _mediator.Send(new GetOwnersQuery());
            return Ok(owners);
        }

        [HttpGet("{id}", Name = "OwnerByIdAsyncCQRS")]
        public async Task<IActionResult> GetOwnerByIdAsync(Guid id)
        {
            var getOwner = new GetOwnerById { Id = id };
            var owner = await _mediator.Send(getOwner);

            var ownerResult = _mapper.Map<OwnerDto>(owner);
            return Ok(ownerResult);
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

                var ownerEntity = await _repository.Owner.GetOwnerByIdAsync(id);
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
                var owner = await _repository.Owner.GetOwnerByIdAsync(id);
                if (owner == null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                if (_repository.Account.AccountsByOwner(id).Any())
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

