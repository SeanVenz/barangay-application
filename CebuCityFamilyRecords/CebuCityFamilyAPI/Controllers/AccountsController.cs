using CebuCityFamilyAPI.Dtos.AccountDto;
using CebuCityFamilyAPI.Dtos.LoginDto;
using CebuCityFamilyAPI.Services.AccountService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CebuCityFamilyAPI.Controllers
{

    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(IAccountService accountService, ILogger<AccountsController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        /// <summary>
        /// Gets All Accounts
        /// </summary>
        /// <returns>Accounts</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/accounts
        ///     {
        ///         "id": 1,
        ///         "firstName": "John",
        ///         "lastName": "Doe",
        ///         "emailAddress": "johndoe@gmail.com",
        ///         "phoneNumber": "0912345678",
        ///         "governmentIdNumber": "12-345-678",
        ///         "password": "燂戻ω᭹ﳞ橚�퐄ള釤잻쪠偪㌇䩾⡠"
        ///     },
        ///     {
        ///         "id": 2,
        ///         "firstName": "Mary",
        ///         "lastName": "Doe",
        ///         "emailAddress": "marydoe@gmail.com",
        ///         "phoneNumber": "09123456789",
        ///         "governmentIdNumber": "12-345-6789",
        ///         "password": "腝嬑叆壗ꄁ砡ﾵ닕쵞拚韹쒼疔鲤"        
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Successfully retrieved Accounts</response>
        /// <response code="204">No Content</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(Name = "GetAllAccounts")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAccount()
        {
            try
            {
                var account = await _accountService.GetAllAccount();

                if (account.IsNullOrEmpty())
                {
                    return NoContent();
                }

                return Ok(account);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets a single Account
        /// </summary>
        /// <param name="id">Account Id</param>
        /// <returns>Returns Account with the Given Id</returns>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     GET /api/accounts/1
        ///     {
        ///         "id": 1,
        ///         "firstName": "John",
        ///         "lastName": "Doe",
        ///         "emailAddress": "johndoe@gmail.com",
        ///         "phoneNumber": "0912345678",
        ///         "governmentIdNumber": "12-345-678",
        ///         "password": "燂戻ω᭹ﳞ橚�퐄ള釤잻쪠偪㌇䩾⡠"
        ///     }
        /// </remarks>
        /// <response code="200">Successfully retrieved an Account</response>
        /// <response code="404">Account with the given id is not found</response>
        /// <response code="500">Internal server error</response>
        [Produces("application/json")]
        [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}", Name = "GetAccountById")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            try
            {
                var account = await _accountService.GetAccountById(id);
                if (account == null)
                    return NotFound($"Account with id {id} does not exist.");

                return Ok(account);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Creates an Account
        /// </summary>
        /// <param name="accountDto">Account Details</param>
        /// <returns>Returns the newly created barangay</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///      POST api/Account
        ///     {
        ///         "First Name": "John",
        ///         "Last Name": "Doe"
        ///         "Email Address" : "johndoe@gmail.com"
        ///         "Phone Number": "09123456789"
        ///         "Government Id Number": "12-345-678"
        ///         "Password" : "Password.123"
        ///     }
        /// 
        /// </remarks>
        /// <response code="201">Successfully created an Account</response>
        /// <response code="400">Account details are invalid</response>
        /// <response code="500">Internal server error</response>
        [HttpPost(Name = "CreateAccount")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AccountDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAccount([FromBody] AccountCreationDto accountDto)
        {
            try
            {
                var newAccount = await _accountService.CreateAccount(accountDto);

                if (newAccount == null)
                {
                    return BadRequest("An account with the same email address or phone number or government ID number already exists.");
                }

                return CreatedAtRoute("GetAccountById", new { id = newAccount.Id }, newAccount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Something went wrong");
            }

        }
        /// <summary>
        /// Logs in to an existing account
        /// </summary>
        /// <param name="model">Log in Details</param>
        /// <returns>Returns a success message if login is successful</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///      POST api/Account/login
        ///     {
        ///         "Email": "johndoe@gmail.com",
        ///         "Password" : "password"
        ///     }
        /// <response code="200">Successfully logged in</response>
        /// <response code="400">Invalid login credentials</response>
        /// <response code="500">Internal server error</response>
        /// </remarks>
        [HttpPost("login")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            try
            {
                var userId = await _accountService.Login(model.Email, model.Password);

                if (userId == null)
                {
                    return BadRequest("Invalid login credentials");
                }

                return Ok("Logged In");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Something went wrong");
            }

        }

        /// <summary>
        /// Deletes an Account
        /// </summary>
        /// <param name="id">Id of the Account</param>
        /// <returns>Returns a message</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE api/accounts/delete/1
        ///         Account with Id = 1 is successfully deleted
        /// 
        /// </remarks>
        /// <response code="200">Successfully deleted an Account</response>
        /// <response code="404">Account with the given id is not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            try
            {
                var account = await _accountService.GetAccountById(id);

                if (account == null)
                {
                    return NotFound($"Account with id {id} does not exist.");
                }

                var deleted = await _accountService.DeleteAccount(id);

                return Ok($"Account with ID = {id} is successfully deleted.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Something went wrong");
            }

        }
    }
}
