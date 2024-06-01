using CebuCityFamilyAPI.Dtos.FamilyMembersDto;
using CebuCityFamilyAPI.Services.FamilyMembersService;
using CebuCityFamilyAPI.Services.FamilyService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CebuCityFamilyAPI.Controllers
{
    [Route("api/members")]
    [ApiController]
    public class FamilyMemberWithDetailsController : ControllerBase
    {
        private readonly IFamilyMembersService _familyMemberService;
        private readonly IFamilyService _familyService;
        private readonly ILogger<FamilyMemberWithDetailsController> _logger;

        public FamilyMemberWithDetailsController(IFamilyMembersService familyMembersService, IFamilyService familyService, ILogger<FamilyMemberWithDetailsController> logger)
        {
            _familyMemberService = familyMembersService;
            _familyService = familyService;
            _logger = logger;
        }

        /// <summary>
        /// Gets All Family Members With their Details
        /// </summary>
        /// <returns>Family Members with their details</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/members/all-with-details
        ///     {
        ///         "id": 5,
        ///         "lastName": "De la Torre",
        ///         "firstName": "Moira",
        ///         "age": 27,
        ///         "maritalStatus": "Single",
        ///         "birthDate": "11-10-99",
        ///         "gender": "Female",
        ///         "occupation": "Artist",
        ///         "contactNo": "0912345678",
        ///         "religion": "Roman Catholic"
        ///     },
        ///     {
        ///         "id": 6,
        ///         "lastName": "De la Torre",
        ///         "firstName": "Fabian",
        ///         "age": 22,
        ///         "maritalStatus": "Single",
        ///         "birthDate": "2-28-01",
        ///         "gender": "Male",
        ///         "occupation": "CPA",
        ///         "contactNo": "09912345678",
        ///         "religion": "Roman Catholic"
        ///     }
        /// </remarks>
        /// <response code="200">Returns the familiy members</response>
        /// <response code="204">No family member found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("all-with-details", Name = "GetAllFamilyMemberWithTheirDetails")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<FamilyMemberWithDetailsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllFamilyMemberWithDetails()
        {
            try
            {
                var task = await _familyMemberService.GetAllFamilyMemberWithDetails();
                if (task.IsNullOrEmpty())
                {
                    return NoContent();
                }
                return Ok(task);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets a single Family Member
        /// </summary>
        /// <param name="id">Family Member Id</param>
        /// <returns>Returns Family Member with the Given Id</returns>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     GET /api/membrs/all-with-details/1
        ///     {
        ///         "id": 1,
        ///         "lastName": "Doe",
        ///         "firstName": "John",
        ///         "age": 31,
        ///         "maritalStatus": "Single",
        ///         "birthDate": "12-23-98",
        ///         "gender": "Male",
        ///         "occupation": "CPA",
        ///         "contactNo": "091234567",
        ///         "religion": "Roman Catholic"
        ///     }
        /// </remarks>
        /// <response code="200">Successfully retrieved the Family Member</response>
        /// <response code="404">Family Member with the given id is not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("all-with-details/{id:int}", Name = "GetAllFamilyMemberWithTheirDetailsById")]
        public async Task<IActionResult> GetAllFamilyMemberWithDetailsById(int id)
        {
            try
            {
                var member = await _familyMemberService.GetFamilyMemberWithDetailsById(id);

                if (member == null)
                {
                    return NotFound($"Family Member with id {id} does not exist.");
                }
                return Ok(member);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets a single Family Member
        /// </summary>
        /// <param name="name">Family member last name</param>
        /// <returns>Returns Family Members with the given Last name</returns>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     GET /api/membrs/all-with-details/Doe
        ///     {
        ///         "id": 1,
        ///         "lastName": "Doe",
        ///         "firstName": "John",
        ///         "age": 31,
        ///         "maritalStatus": "Single",
        ///         "birthDate": "12-23-98",
        ///         "gender": "Male",
        ///         "occupation": "CPA",
        ///         "contactNo": "091234567",
        ///         "religion": "Roman Catholic"
        ///     }
        /// </remarks>
        /// <response code="200">Successfully retrieved the Family Member</response>
        /// <response code="404">Family Member with the given name is not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("all-with-details/{name}", Name = "GetAllFamilyMemberWithTheirDetailsByName")]
        public async Task<IActionResult> GetAllFamilyMemberWithDetailsByName(string name)
        {
            try
            {
                var member = await _familyMemberService.GetFamilyMemberWithDetailsByName(name);

                if (member == null)
                {
                    return NotFound($"Family Member with id {name} does not exist.");
                }
                return Ok(member);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Creates a Family Member with their Details
        /// </summary>
        /// <param name="id">Family Id</param>
        /// <param name="familyMemberDto">Family Member Model</param>
        /// <returns>Returns the newly created family member</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///      POST api/Account
        ///     {
        ///         "lastName": "Doe",
        ///         "firstName": "John",
        ///         "age": 31,
        ///         "maritalStatus": "Single",
        ///         "birthDate": "12-23-98",
        ///         "gender": "Male",
        ///         "occupation": "CPA",
        ///         "contactNo": "091234567",
        ///         "religion": "Roman Catholic"
        ///     }
        /// 
        /// </remarks>
        /// <response code="201">Successfully created an Account</response>
        /// <response code="400">Account details are invalid</response>
        /// <response code="404">Family ID is invalid</response>
        /// <response code="500">Internal server error</response>
        [HttpPost("{id}/family-members-with-details", Name = "CreateFamilyMemberWithDetails")]
        public async Task<IActionResult> CreateFamilyMemberWithDetails(int id, [FromBody] FamilyMemberWithDetailsCreationDto familyMemberDto)
        {
            try
            {
                var check = await _familyService.GetFamilyById(id);
                if (check == null)
                {
                    return NotFound($"Family with ID {id} does not exist");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            try
            {

                var newFamilyMemberWithDetails = await _familyMemberService.CreateFamilyMemberWithDetails(id, familyMemberDto);

                return CreatedAtRoute("GetAllFamilyMemberWithTheirDetailsById", new { id = newFamilyMemberWithDetails.Id }, newFamilyMemberWithDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Updates a Family Member
        /// </summary>
        /// <param name="familyMember">Family Member Details</param>
        /// <param name="id">id of Family Member</param>
        /// <returns>Returns the updated Family member</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///      PUT api/members/1/family-members-with-details
        ///     {
        ///         "lastName": "Doe",
        ///         "firstName": "John",
        ///         "age": 31,
        ///         "maritalStatus": "Single",
        ///         "birthDate": "12-23-98",
        ///         "gender": "Male",
        ///         "occupation": "CPA",
        ///         "contactNo": "091234567",
        ///         "religion": "Roman Catholic"
        ///     }
        /// 
        /// </remarks>
        /// <response code="202">Successfully updated a Family Member</response>
        /// <response code="400">Updated Barangay details are invalid</response>
        /// <response code="400">Family ID is invalid</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}", Name = "update-family-member-with-details")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(FamilyMemberWithDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateFamilyMember(int id, [FromBody] FamilyMemberWithDetailsUpdationDto familyMember)
        {
            try
            {
                var check = await _familyMemberService.GetFamilyMemberWithDetailsById(id);
                if (check == null)
                    return NotFound($"Family with Id = {id} is not found");

                var update = await _familyMemberService.UpdateFamilyMemberWithDetails(id, familyMember);
                var getUpdated = await _familyMemberService.GetFamilyMemberWithDetailsById(id);
                return Ok(getUpdated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Deletes a Family Member
        /// </summary>
        /// <param name="id">Id of the Family member</param>
        /// <returns>Returns a message</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE api/members/delete/family-members-with-details/1
        ///         Family Member with Id = 1 is successfully deleted
        /// 
        /// </remarks>
        /// <response code="200">Successfully deleted Family member</response>
        /// <response code="404">Family member with the given id is not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("delete-member-with-details/{id}", Name = "DeleteMemberWithDetails")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteFamilyMember(int id)
        {

            try
            {
                var check = await _familyMemberService.GetFamilyMemberWithDetailsById(id);
                if (check == null)
                {
                    return NotFound($"Family Member with Id = {id} does not exist!");
                }
                var deleted = await _familyMemberService.DeleteFamilyMemberWithDetails(id);
                return Ok($"Family Member with Id = {id} is successfully deleted");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}
