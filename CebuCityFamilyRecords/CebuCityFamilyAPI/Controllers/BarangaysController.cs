using CebuCityFamilyAPI.Dtos.BarangayDto;
using CebuCityFamilyAPI.Dtos.FamilyDto;
using CebuCityFamilyAPI.Services.BarangayService;
using CebuCityFamilyAPI.Services.FamilyService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CebuCityFamilyAPI.Controllers
{
    [Route("api/barangays")]
    [ApiController]
    public class BarangaysController : ControllerBase
    {
        private readonly IBarangayService _barangayService;
        private readonly IFamilyService _familyService;
        private readonly ILogger<BarangaysController> _logger;

        public BarangaysController(IBarangayService barangayService, IFamilyService familyService, ILogger<BarangaysController> logger)
        {
            _barangayService = barangayService;
            _familyService = familyService;
            _logger = logger;
        }

        /// <summary>
        /// Gets All Barangays
        /// </summary>
        /// <returns>List of Barangays</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/barangays
        ///     {
        ///         "id": 1,
        ///         "name": "Tisa",
        ///         "captain": "Arlee Cathesyed"
        ///     },
        ///     {
        ///         "id": 2,
        ///         "name": "Lahug",
        ///         "captain": "Brander Andrich"    
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Successfully retrieved Barangays</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(Name = "GetAllBarangays")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BarangayDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBarangays()
        {
            try
            {
                var barangays = await _barangayService.GetAllBarangays();
                if (barangays.IsNullOrEmpty())
                {
                    return NoContent();
                }
                return Ok(barangays);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Gets the Families living in the Barangay by ID
        /// </summary>
        /// <returns>Barangay</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/barangays/1/families
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
        /// <response code="200">Successfully retrieved Families in Barangay</response>
        /// <response code="404">Barangay with the given id is not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}/families", Name = "GetBarangayById")]
        public async Task<IActionResult> GetBarangayById(int id)
        {
            try
            {
                var barangay = await _barangayService.GetBarangayById(id);
                if (barangay == null)
                    return NotFound($"Barangay with id {id} does not exist.");

                var families = await _familyService.GetFamilyByBarangayId(id);
                if (!families.Any())
                    return NoContent();
                return Ok(families);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Gets the Total Population living in the Barangay by ID
        /// </summary>
        /// <returns>Total population of a Barangay</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/barangays/population/1
        ///         10
        /// </remarks>
        /// <response code="200">Successfully retrieved Families in Barangay</response>
        /// <response code="404">Barangay with the given id is not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("population/{id}", Name = "GetPopulationByBarangayId")]
        public async Task<IActionResult> GetBarangayPopulationById(int id)
        {
            try
            {
                var barangay = await _barangayService.GetBarangayById(id);
                if (barangay == null)
                    return NotFound($"Barangay with id {id} does not exist.");

                var population = await _barangayService.GetPopulationInBarangay(id);
                return Ok(population);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Gets the Families living in the Barangay by the Barangay Name
        /// </summary>
        /// <returns>Barangay</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/barangays/search?name=Tisa
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
        /// <response code="200">Successfully retrieved Families in Barangay</response>
        /// <response code="404">Barangay with the given id is not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("search/{name}", Name = "GetBarangayByName")]
        public async Task<IActionResult> GetBarangayByName(String name)
        {
            try
            {
                var barangay = await _barangayService.GetBarangayByName(name);
                if (barangay == null)
                    return NotFound($"Barangay {name} does not exist.");

                return Ok(barangay);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }
        /// <summary>
        /// Creates a Barangay
        /// </summary>
        /// <param name="barangayDto">barangay details</param>
        /// <returns>Returns the newly created barangay</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///      POST api/Barangays
        ///     {
        ///         "name": "Cansojong",
        ///         "captain": "Delwin Mateo Gauson"
        ///     }
        /// 
        /// </remarks>
        /// <response code="201">Successfully created a Barangay</response>
        /// <response code="400">Barangay details are invalid</response>
        /// <response code="500">Internal server error</response>
        [HttpPost(Name = "CreateBarangay")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BarangayDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateBarangay([FromBody] BarangayCreationDto barangayDto)
        {
            try
            {
                var count = await _barangayService.CountBarangayName(barangayDto.Name!);
                if (count > 0)
                {
                    return StatusCode(409, "Barangay already exists");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
            try
            {
                var newBarangay = await _barangayService.CreateBarangay(barangayDto);
                return CreatedAtRoute("GetBarangayById", new { id = newBarangay.Id }, newBarangay);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }
        /// <summary>
        /// Registers a Family to a Barangay
        /// </summary>
        /// <param name="newFamily">family details</param>
        /// <param name="id">Id of Barangay</param>
        /// <returns>Returns the newly registered family to barangay</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///      POST /api/Barangays/1/register
        ///     {
        ///         "name": "Gauson",
        ///         "sitio": "Ibacan"
        ///     }
        /// 
        /// </remarks>
        /// <response code="201">Successfully registered a Family in Barangay</response>
        /// <response code="400">Family details are invalid</response>
        /// <response code="500">Internal server error</response>
        [HttpPost("{id}/family/register", Name = "RegisterFamilyToBarangay")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BarangayDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterFamily(int id, [FromBody] FamilyCreationDto newFamily)
        {
            try
            {
                var barangay = await _barangayService.GetBarangayById(id);
                if (barangay == null)
                {
                    return NotFound($"Barangay with ID {id} does not exist");
                }
                var newFamilyId = await _familyService.Register(id, newFamily);
                return CreatedAtAction(
                    nameof(FamiliesController.GetFamilies),
                    "Families",
                    new { id = newFamilyId },
                    $"Successfully registered {newFamily.Name} Family");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }
        /// <summary>
        /// Updates a Barangay using Barangay ID
        /// </summary>
        /// <param name="barangay">barangay details</param>
        /// <param name="id">id of barangay</param>
        /// <returns>Returns the updated barangay</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///      PUT api/Barangays/5
        ///     {
        ///         "name": "Tisa",
        ///         "captain": "Phillip Savior Zafra"
        ///     }
        /// 
        /// </remarks>
        /// <response code="202">Successfully updated a Barangay</response>
        /// <response code="400">Updated Barangay details are invalid</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}", Name = "UpdateBarangay")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BarangayDto), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateBarangay(int id, [FromBody] BarangayUpdationDto barangay)
        {
            try
            {
                var dbBarangay = await _barangayService.GetBarangayById(id);
                if (dbBarangay == null)
                    return NotFound($"Barangay with ID {id} does not exist");
                var updatedBarangay = await _barangayService.UpdateBarangay(id, barangay);
                return Ok(updatedBarangay);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Updates a Barangay using Barangay Name
        /// </summary>
        /// <param name="barangay">barangay details</param>
        /// <param name="name">name of barangay</param>
        /// <returns>Returns the updated barangay</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///      PUT api/Barangays/Tisa
        ///     {
        ///         "name": "Dumlog",
        ///         "captain": "John Doe"
        ///     }
        /// 
        /// </remarks>
        /// <response code="202">Successfully updated a Barangay</response>
        /// <response code="400">Updated Barangay details are invalid</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{name}/update", Name = "UpdateBarangayByName")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BarangayDto), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateBarangayByName(string name, [FromBody] BarangayUpdationDto barangay)
        {
            try
            {
                var dbBarangay = await _barangayService.GetBarangayByName(name);
                if (dbBarangay == null)
                    return NotFound($"Barangay with Name {name} does not exist");
                var updatedBarangay = await _barangayService.UpdateBarangayByName(name, barangay);
                return Ok(updatedBarangay);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }
        /// <summary>
        /// Deletes a Barangay by Name
        /// </summary>
        /// <returns>Deleted Barangay</returns>
        /// <response code="202">Successfully deleted a Barangay</response>
        /// <response code="400">Barangay Name invalid</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{name}/delete", Name = "DeleteBarangay")]
        [ProducesResponseType(typeof(BarangayDto), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteBarangay(String name)
        {
            try
            {
                var dbBarangay = await _barangayService.GetBarangayByName(name);
                if (dbBarangay == null)
                    return NotFound($"Barangay with Name {name} does not exist");
                await _barangayService.DeleteBarangay(name);
                return Ok("Barangay successfully deleted");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
