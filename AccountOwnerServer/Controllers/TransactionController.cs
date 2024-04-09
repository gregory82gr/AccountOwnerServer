using AccountOwnerServer.Validation;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace AccountOwnerServer.Controllers
{
    [Route("api/transaction")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        private readonly OwnerIdValidation _validation;
        public TransactionController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _validation = new OwnerIdValidation();
        }

        [HttpGet]
        public IActionResult GetAllTransactions()
        {
            //return NotFound();
            //return StatusCode(500, "Some message");
            //return BadRequest("This is bad request");
            try
            {
                var transactions = _repository.Transaction.GetAllTransactions();
                _logger.LogInfo($"Returned all transactions from database.");

                var transactionsResult = _mapper.Map<IEnumerable<TransactionDto>>(transactions);
                return Ok(transactionsResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllTransactions action: {ex.Message}");

                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "TransactionById")]
        public IActionResult GetTransactionById(Guid id)
        {
            try
            {
                var transaction = _repository.Transaction.GetTransactionById(id);
                if (transaction is null)
                {
                    _logger.LogError($"Transaction with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Transaction with id: {id}");

                    var transactionResult = _mapper.Map<TransactionDto>(transaction);
                    return Ok(transactionResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetTransactionById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("owner/{id}", Name = "TransactionByOwnerId")]
        public IActionResult GetTransactionsByOwner(Guid id)
        {
            try
            {
                var transaction = _repository.Transaction.GetTransactionsByOwner(id);
                if (transaction is null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Owner with id: {id}");

                   var transactionResult = _mapper.Map<IEnumerable<TransactionDto>>(transaction);
                    return Ok(transactionResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetTransactionsByOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateTransaction([FromBody] TransactionForCreationDto transaction)
        {
            //return BadRequest("This is bad request");
            try
            {
                if (transaction is null)
                {
                    _logger.LogError("Transaction object sent from client is null.");
                    return BadRequest("Transaction object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Transaction object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var transactionEntity = _mapper.Map<Transaction>(transaction);

                _repository.Transaction.CreateTransaction(transactionEntity);
                _repository.Save();

                var createdTransaction = _mapper.Map<TransactionDto>(transactionEntity);

                return CreatedAtRoute("TransactionById", new { id = createdTransaction.Id }, createdTransaction);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateTransaction action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTransaction(Guid id, [FromBody] TransactionForUpdateDto transaction)
        {
            try
            {
                if (transaction is null)
                {
                    _logger.LogError("Transaction object sent from client is null.");
                    return BadRequest("Transaction object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Transaction object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var transactionEntity = _repository.Transaction.GetTransactionById(id);
                if (transactionEntity is null)
                {
                    _logger.LogError($"Account with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(transaction, transactionEntity);

                _repository.Transaction.UpdateTransaction(transactionEntity);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateTransaction action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTransaction(Guid id)
        {
            try
            {
                if (!_validation.IsValid(id.ToString()))
                {
                    _logger.LogError($"Transaction with id: {id}, is invalid.");
                    ModelState.AddModelError("Transaction", "Transaction Number is invalid");
                    return BadRequest();
                }
                var transaction = _repository.Transaction.GetTransactionById(id);
                if (transaction == null)
                {
                    _logger.LogError($"Transaction with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Transaction.DeleteTransaction(transaction);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteTransaction action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
