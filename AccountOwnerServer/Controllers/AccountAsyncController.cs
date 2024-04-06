using AccountOwnerServer.Validation;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace AccountOwnerServer.Controllers
{
    [Route("api/accountAsync")]
    [ApiController]
    public class AccountAsyncController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapperAsync _repository;
        private IMapper _mapper;
        private readonly OwnerIdValidation _validation;

        public AccountAsyncController(ILoggerManager logger, IRepositoryWrapperAsync repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _validation = new OwnerIdValidation();
        }

        [HttpGet]
        //[AllowAnonymous]
        public async Task<IActionResult> GetAllAccountsAsync()
        {
            //return NotFound();
            //return StatusCode(500, "Some message");
            //return BadRequest("This is bad request");

            try
            {
                var accounts = await _repository.Account.GetAllAccountsAsync();
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

        [HttpGet("{id}", Name = "AccountByIdAsync")]
        public async Task<IActionResult> GetAccountByIdAsync(Guid id)
        {
            try
            {
                var account = await _repository.Account.GetAccountByIdAsync(id);
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
                System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken);
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

                _repository.Account.CreateAccount(accountEntity);
                await _repository.SaveAsync(cancellationToken);

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
        public async  Task<IActionResult> UpdateAccountAsync(Guid id, [FromBody] AccountForUpdateDto account)
        {
            try
            {
                System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken);
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

                _repository.Account.UpdateAccount(accountEntity);
                await _repository.SaveAsync(cancellationToken);

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
                System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken);
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

                _repository.Account.DeleteAccount(account);
               await _repository.SaveAsync(cancellationToken);

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
