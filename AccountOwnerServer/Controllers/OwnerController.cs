﻿using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace AccountOwnerServer.Controllers
{
    [Route("api/owner")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private ILoggerManager _logger; 
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        private IUnitOfWork _unitOfWork;
        public OwnerController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper, IUnitOfWork unitOfWork) 
        { 
            _logger = logger; 
            _repository = repository; 
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet] 
        public IActionResult GetAllOwners() 
        {
            //return NotFound();
            //return StatusCode(500, "Some message");
            //return BadRequest("This is bad request");
            try
            { 
                var owners = _unitOfWork.RepositoryWrapper.Owner.GetAllOwners(); 
                _logger.LogInfo($"Returned all owners from database.");

                var ownersResult = _mapper.Map<IEnumerable<OwnerDto>>(owners);
                return Ok(ownersResult); 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError($"Something went wrong inside GetAllOwners action: {ex.Message}"); 
                
                return StatusCode(500, "Internal server error"); 
            } 
        }

        [HttpGet("{id}", Name = "OwnerById")]
        public IActionResult GetOwnerById(Guid id)
        {
            try
            {
                var owner = _unitOfWork.RepositoryWrapper.Owner.GetOwnerById(id);
                if (owner is null)
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
        public IActionResult GetOwnerWithDetails(Guid id)
        {
            try
            {
                var owner = _unitOfWork.RepositoryWrapper.Owner.GetOwnerWithDetails(id);
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
        public IActionResult CreateOwner([FromBody] OwnerForCreationDto owner)
        {
            //return BadRequest("This is bad request");
            try
            {
                if (owner is null)
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

                //_repository.Owner.CreateOwner(ownerEntity);
                _unitOfWork.RepositoryWrapper.Owner.CreateOwner(ownerEntity);
                //_repository.Save();
                if (!_unitOfWork.Commit()) _unitOfWork.Rollback();

                var createdOwner = _mapper.Map<OwnerDto>(ownerEntity);

                return CreatedAtRoute("OwnerById", new { id = createdOwner.Id }, createdOwner);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("withAccounts")]
        public IActionResult CreateOwnerWithAccounts([FromBody] OwnerWithAccountsForCreationDto owner)
        {
            //return BadRequest("This is bad request");
            try
            {
                if (owner is null)
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

                //_repository.Owner.CreateOwner(ownerEntity);
                _unitOfWork.RepositoryWrapper.Owner.CreateOwner(ownerEntity);
                //_repository.Save();
                if (!_unitOfWork.Commit()) _unitOfWork.Rollback();

                var createdOwner = _mapper.Map<OwnerDto>(ownerEntity);

                return CreatedAtRoute("OwnerById", new { id = createdOwner.Id }, createdOwner);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateOwnerWithAccounts action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOwner(Guid id, [FromBody] OwnerForUpdateDto owner)
        {
            try
            {
                if (owner is null)
                {
                    _logger.LogError("Owner object sent from client is null.");
                    return BadRequest("Owner object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid owner object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var ownerEntity = _unitOfWork.RepositoryWrapper.Owner.GetOwnerById(id);
                if (ownerEntity is null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(owner, ownerEntity);
                _unitOfWork.RepositoryWrapper.Owner.UpdateOwner(ownerEntity);
                if (!_unitOfWork.Commit()) _unitOfWork.Rollback();
                //_repository.Owner.UpdateOwner(ownerEntity);
                //_repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOwner(Guid id)
        {
            try
            {
                var owner = _repository.Owner.GetOwnerById(id);
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

                //_repository.Owner.DeleteOwner(owner);
                //_repository.Save();

                _unitOfWork.RepositoryWrapper.Owner.DeleteOwner(owner);
                if (!_unitOfWork.Commit()) _unitOfWork.Rollback();

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
