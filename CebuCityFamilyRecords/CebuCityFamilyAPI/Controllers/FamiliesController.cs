using CebuCityFamilyAPI.Dtos.FamilyDto;
using CebuCityFamilyAPI.Services.FamilyMembersService;
using CebuCityFamilyAPI.Services.FamilyService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CebuCityFamilyAPI.Controllers
{
    [Route("api/families")]
    [ApiController]
    public class FamiliesController : ControllerBase
    {
        private readonly IFamilyService _familyService;
        private readonly IFamilyMembersService _familyMembersService;
        private readonly ILogger<FamiliesController> _logger;

        public FamiliesController(IFamilyService familyService, IFamilyMembersService familyMembersService, ILogger<FamiliesController> logger)
        {
            _familyService = familyService;
            _familyMembersService = familyMembersService;
            _logger = logger;
        }
        /// <summary>
        /// Gets all Families
        /// </summary>
        /// <returns>List of Families</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/families
        ///     {
        ///         "id": 1,
        ///         "name": "Uppett",
        ///         "sitio": "Sidlakan"
        ///     },
        ///     {
        ///         "id": 2,
        ///         "name": "Pedro",
        ///         "sitio": "Motra"   
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Successfully retrieved Barangays</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(Name = "GetAllFamilies")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(FamilyDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFamilies()
        {
            try
            {
                var families = await _familyService.GetAllFamilies();
                if (families.IsNullOrEmpty())
                {
                    return NoContent();
                }
                return Ok(families);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }
        /// <summary>
        /// Gets the Family Members with their Details by their Family ID
        /// </summary>
        /// <returns>Family</returns>
        [HttpGet("{id}/members", Name = "GetFamilyById")]
        public async Task<IActionResult> GetFamilyById(int id)
        {
            try
            {
                var family = await _familyService.GetFamilyById(id);
                if (family == null)
                    return NotFound($"Family with ID {id} does not exist.");

                var familyMembers = await _familyMembersService.GetFamilyMembersById(id);
                if (!familyMembers.Any())
                    return NoContent();
                return Ok(familyMembers);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets the Family Members with their Details by their Barangay Name
        /// </summary>
        /// <returns>Family</returns>
        [HttpGet("search", Name = "GetFamilyByName")]
        public async Task<IActionResult> GetFamilyByBarangayName([FromQuery] String name)
        {
            try
            {
                var family = await _familyService.GetFamilyByBarangayName(name);

                if (!family.Any())
                    return NotFound($"{name} Barangay does not exist.");

                var familyMembers = await _familyMembersService.GetFamilyMembersByName(name);
                if (!familyMembers.Any())
                    return NoContent();
                return Ok(familyMembers);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        //}
        /// <summary>
        /// Updates where a Family lives
        /// </summary>
        /// <param name="familyToUpdate">details</param>
        /// <param name="id">Id of family to update</param>
        /// <returns>Returns updated family</returns>
        /// <remarks>
        ///      PUT api/FamiliesController/5
        ///      {
        ///         "sitio": "Sidlakan"
        ///      }
        /// </remarks>
        /// <response code="202">Successfully updated a family</response>
        /// <response code="400">Updated family detail is invalid</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}/update", Name = "UpdateFamily")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(FamilyDto), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateFamily(int id, [FromBody] FamilyUpdationDto familyToUpdate)
        {
            try
            {

                var updateFamily = await _familyService.GetFamilyById(id);

                if (updateFamily == null)
                    return NotFound($"Family with ID {id} does not exist");

                var updatedFamily = await _familyService.UpdateFamily(id, familyToUpdate);
                return Ok(updatedFamily);

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Deletes a Family by their ID
        /// </summary>
        /// <returns>Deleted Family</returns>
        /// <response code="202">Successfully deleted a family</response>
        /// <response code="400">Family id invalid</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}/delete", Name = "DeleteFamilyById")]
        [ProducesResponseType(typeof(FamilyDto), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteFamilyById(int id)
        {
            try
            {
                var familyToDelete = await _familyService.GetFamilyById(id);
                if (familyToDelete == null)
                    return NotFound($"Family with ID {id} does not exist!");

                await _familyService.DeleteFamilyById(id);
                return Ok("Family Successfully Deleted!");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Retrieves all families with the corresponding member and their details
        /// </summary>
        /// <returns>List of family with member</returns>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET api/families/families-with-members
        ///     {
        ///         "id": 1,
        ///         "name": "Pedro",
        ///         "sitio": "Motra",
        ///         "familyMembers": [
        ///             {
        ///                 "id": 1,
        ///                 "lastName": "De la Torre",
        ///                 "firstName": "Moira",
        ///                 "age": 27,
        ///                 "maritalStatus": "Single",
        ///                 "birthDate": "12-23-96",
        ///                 "gender": "Female",
        ///                 "occupation": "Artist",
        ///                 "contactNo": "0912345678",
        ///                 "religion": "Roman Catholic"
        ///             },
        ///             {
        ///                 "id": 2,
        ///                 "lastName": "De la Torre",
        ///                 "firstName": "Fabian",
        ///                 "age": 22,
        ///                 "maritalStatus": "Single",
        ///                 "birthDate": "2-28-01",
        ///                 "gender": "Male",
        ///                 "occupation": "CPA",
        ///                 "contactNo": "09912345678",
        ///                 "religion": "Roman Catholic"
        ///             }
        ///         ]
        ///     },
        ///     {
        ///         "id": 2,
        ///         "name": "Pedra",
        ///         "sitio": "Motra",
        ///         "familyMembers": [
        ///             ]
        ///     }
        /// </remarks>
        /// <response code="200">Returns the list of families with their members</response>
        /// <response code="204">No families with members found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("families-with-members", Name = "GetFamiliesWithMembers")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<FamilyWithMemberDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFamiliesWithMembers()
        {
            try
            {
                var familiesWithMembers = await _familyService.GetFamilyWithFamilyMembers();
                if (familiesWithMembers.IsNullOrEmpty())
                {
                    return NoContent();
                }
                return Ok(familiesWithMembers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Something went wrong: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves all families with the corresponding member and their details
        /// </summary>
        /// <param name="id">ID of the family</param>
        /// <returns>Family with member</returns>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET api/families/families-with-members/1
        ///     {
        ///         "id": 1,
        ///         "name": "Pedro",
        ///         "sitio": "Motra",
        ///         "familyMembers": [
        ///             {
        ///                 "id": 1,
        ///                 "lastName": "De la Torre",
        ///                 "firstName": "Moira",
        ///                 "age": 27,
        ///                 "maritalStatus": "Single",
        ///                 "birthDate": "12-23-96",
        ///                 "gender": "Female",
        ///                 "occupation": "Artist",
        ///                 "contactNo": "0912345678",
        ///                 "religion": "Roman Catholic"
        ///             },
        ///             {
        ///                 "id": 2,
        ///                 "lastName": "De la Torre",
        ///                 "firstName": "Fabian",
        ///                 "age": 22,
        ///                 "maritalStatus": "Single",
        ///                 "birthDate": "2-28-01",
        ///                 "gender": "Male",
        ///                 "occupation": "CPA",
        ///                 "contactNo": "09912345678",
        ///                 "religion": "Roman Catholic"
        ///             }
        ///         ]
        ///     }
        /// </remarks>
        /// <response code="200">Returns the familiy with their members</response>
        /// <response code="204">No family found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("families-with-members/{id:int}", Name = "GetFamiliesWithMembersById")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<FamilyWithMemberDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFamiliesWithMembersById(int id)
        {
            try
            {
                var familiesWithMembers = await _familyService.GetFamilyWithFamilyMembersById(id);
                if (familiesWithMembers == null)
                {
                    return NotFound($"Family with ID {id} does not exist.");
                }
                return Ok(familiesWithMembers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Something went wrong: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves all families with the corresponding member and their details
        /// </summary>
        /// <param name="name">Name of the family</param>
        /// <returns>Family with member</returns>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET api/families/families-with-members/Pedro
        ///     {
        ///         "id": 1,
        ///         "name": "Pedro",
        ///         "sitio": "Motra",
        ///         "familyMembers": [
        ///             {
        ///                 "id": 1,
        ///                 "lastName": "De la Torre",
        ///                 "firstName": "Moira",
        ///                 "age": 27,
        ///                 "maritalStatus": "Single",
        ///                 "birthDate": "12-23-96",
        ///                 "gender": "Female",
        ///                 "occupation": "Artist",
        ///                 "contactNo": "0912345678",
        ///                 "religion": "Roman Catholic"
        ///             },
        ///             {
        ///                 "id": 2,
        ///                 "lastName": "De la Torre",
        ///                 "firstName": "Fabian",
        ///                 "age": 22,
        ///                 "maritalStatus": "Single",
        ///                 "birthDate": "2-28-01",
        ///                 "gender": "Male",
        ///                 "occupation": "CPA",
        ///                 "contactNo": "09912345678",
        ///                 "religion": "Roman Catholic"
        ///             }
        ///         ]
        ///     }
        /// </remarks>
        /// <response code="200">Returns the familiy with their members</response>
        /// <response code="204">No family found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("families-with-members/{name}", Name = "GetFamiliesWithMembersByName")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(FamilyWithMemberDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFamiliesWithMembersByName(string name)
        {
            try
            {
                var familiesWithMembers = await _familyService.GetFamilyWithFamilyMembersByName(name);
                if (familiesWithMembers == null)
                {
                    return NotFound($"Family with Name {name} does not exist.");
                }
                return Ok(familiesWithMembers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Something went wrong: {ex.Message}");
            }
        }
    }
}
