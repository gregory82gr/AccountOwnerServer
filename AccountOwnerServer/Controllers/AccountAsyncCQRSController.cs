using AccountOwnerServer.CQRS.Commands.Accounts;
using AccountOwnerServer.CQRS.Commands.Owners;
using AccountOwnerServer.CQRS.Queries.Accounts;
using AccountOwnerServer.CQRS.Queries.Owners.Queries;
using AccountOwnerServer.Validation;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace AccountOwnerServer.Controllers
{
    [Route("api/accountAsyncCQRS")]
    [ApiController]
    public class AccountAsyncCQRSController : ControllerBase
    {
        private readonly IMediator _mediator;
        private ILoggerManager _logger;
        private IRepositoryWrapperAsync _repository;
        private IMapper _mapper;
        private readonly OwnerIdValidation _validation;

        public AccountAsyncCQRSController(ILoggerManager logger, IRepositoryWrapperAsync repository, IMapper mapper, IMediator mediator)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
            _validation = new OwnerIdValidation();
        }

        [HttpGet]
        public async Task<IActionResult> GetAccountsAsync()
        {
            try
            {
                var accounts = await _mediator.Send(new GetAccountsQuery());
                _logger.LogInfo($"Returned all accounts from database.");

                var accountsResult = _mapper.Map<IEnumerable<AccountDto>>(accounts);
                return Ok(accountsResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllAccountsAsync action: {ex.Message}");

                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "AccountByIdAsyncCQRS")]
        public async Task<IActionResult> GetAccountByIdAsync(Guid id)
        {
            try
            {
                var getAccount = new GetAccountByIdQuery { Id = id };
                var account = await _mediator.Send(getAccount); 
                if (account is null)
                {
                    _logger.LogError($"Account with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned account with id: {id}");

                    var accountResult = _mapper.Map<AccountDto>(account);
                    return Ok(accountResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAccountByIdAsync action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccountAsync([FromBody] AccountForCreationDto account)
        {
            //return BadRequest("This is bad request");
            try
            {
                if (account is null)
                {
                    _logger.LogError("Account object sent from client is null.");
                    return BadRequest("Account object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid account object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var accountEntity = _mapper.Map<Account>(account);
                await _mediator.Send(new CreateAccountCommand(accountEntity));

                var createdAccount = _mapper.Map<AccountDto>(accountEntity);
              
                return CreatedAtRoute("AccountById", new { id = createdAccount.Id }, createdAccount);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateAccountAsync action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccountAsync(Guid id, [FromBody] AccountForUpdateDto account)
        {
            try
            {
               
                if (account is null)
                {
                    _logger.LogError("Account object sent from client is null.");
                    return BadRequest("Account object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Account object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var accountEntity = await _repository.Account.GetAccountByIdAsync(id);
                if (accountEntity is null)
                {
                    _logger.LogError($"Account with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(account, accountEntity);
                

                await _mediator.Send(new UpdateAccountCommand(accountEntity));

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateAccountAsync action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccountAsync(Guid id)
        {
            try
            {
               
                if (!_validation.IsValid(id.ToString()))
                {
                    _logger.LogError($"Account with id: {id}, hasn't been found in db.");
                    ModelState.AddModelError("AccountNumber", "Account Number is invalid");
                    return BadRequest();
                }
                var account = await _repository.Account.GetAccountByIdAsync(id);
                if (account == null)
                {
                    _logger.LogError($"Account with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                await _mediator.Send(new DeleteAccountCommand(account));

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteAccountAsync action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
