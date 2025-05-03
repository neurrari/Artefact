using Artefact.API.Data.Dtos.Account;
using Artefact.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Artefact.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountReadModel>>> GetAccounts()
        {
            var accounts = await _accountService.GetAllAsync();
            return Ok(accounts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccountReadModel>> GetAccount(int id)
        {
            var account = await _accountService.GetByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

        [HttpPost]
        public async Task<ActionResult<AccountReadModel>> PostAccount(AccountCreateModel accountDto)
        {
            try
            {
                var createdAccount = await _accountService.CreateAsync(accountDto);
                return CreatedAtAction(nameof(GetAccount), new { id = createdAccount.Id }, createdAccount);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            // Add other specific exceptions if needed
            catch (Exception ex)
            {
                // Log the exception details (not shown here)
                return StatusCode(500, "An error occurred while creating the account.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(int id, AccountUpdateModel accountDto)
        {
            try
            {
                var success = await _accountService.UpdateAsync(id, accountDto);
                if (!success)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            // Add other specific exceptions if needed
            catch (Exception ex)
            {
                // Log the exception details (not shown here)
                return StatusCode(500, "An error occurred while updating the account.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            try
            {
                var success = await _accountService.DeleteAsync(id);
                if (!success)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception details (not shown here)
                return StatusCode(500, "An error occurred while deleting the account.");
            }
        }
    }
}
