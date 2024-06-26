﻿using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace AccountOwnerServer.Controllers
{
    [Route("api/ownerAsync")]
    [ApiController]
    public class OwnerAsyncController : ControllerBase
    {
        private ILoggerManager _logger;
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public OwnerAsyncController(ILoggerManager logger, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOwnersAsync()
        {
            try
            {
                var owners = await _unitOfWork.RepositoryWrapper.Owner.GetAllOwnersAsync();
                _logger.LogInfo($"Returned all owners from database.");
                var ownersResult = _mapper.Map<IEnumerable<OwnerDto>>(owners);
                return Ok(ownersResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllOwnersAsync action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "OwnerByIdAsync")]
        public async Task<IActionResult> GetOwnerByIdAsync(Guid id)
        {
            try
            {
                var owner = await _unitOfWork.RepositoryWrapper.Owner.GetOwnerByIdAsync(id);
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
                _logger.LogError($"Something went wrong inside GetOwnerById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}/account")]
        public async Task<IActionResult> GetOwnerWithDetailsAsync(Guid id)
        {
            try
            {
                var owner = await _unitOfWork.RepositoryWrapper.Owner.GetOwnerWithDetailsAsync(id);
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
        public async Task<IActionResult> CreateOwnerAsync([FromBody] OwnerForCreationDto owner)
        {
            try
            {
                System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken);
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

                var ownerEntity = _mapper.Map<Owner>(owner);

                _unitOfWork.RepositoryWrapper.Owner.CreateOwner(ownerEntity);
                await _unitOfWork.RepositoryWrapper.SaveAsync(cancellationToken);

                var createdOwner = _mapper.Map<OwnerDto>(ownerEntity);

                return CreatedAtRoute("OwnerById", new { id = createdOwner.Id }, createdOwner);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOwnerAsync(Guid id, [FromBody] OwnerForUpdateDto owner)
        {
            try
            {
                System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken);
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

                var ownerEntity = await _unitOfWork.RepositoryWrapper.Owner.GetOwnerByIdAsync(id);
                if (ownerEntity == null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(owner, ownerEntity);

                _unitOfWork.RepositoryWrapper.Owner.UpdateOwner(ownerEntity);
                await _unitOfWork.RepositoryWrapper.SaveAsync(cancellationToken);

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
                System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken);
                var owner = await _unitOfWork.RepositoryWrapper.Owner.GetOwnerByIdAsync(id);
                if (owner == null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                var accounts = await _unitOfWork.RepositoryWrapper.Account.AccountsByOwnerAsync(id);
                if (accounts.Any())
                {
                    _logger.LogError($"Cannot delete owner with id: {id}. It has related accounts. Delete those accounts first");
                    return BadRequest("Cannot delete owner. It has related accounts. Delete those accounts first");
                }

                _unitOfWork.RepositoryWrapper.Owner.DeleteOwner(owner);
                await _unitOfWork.RepositoryWrapper.SaveAsync(cancellationToken);

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
