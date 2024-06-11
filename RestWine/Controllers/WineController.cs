using Microsoft.AspNetCore.Mvc;
using WineLib;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestWine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WineController : ControllerBase
    {
        private readonly WineRepository _wineRepository;
        public WineController(WineRepository wineRepository)
        {
            _wineRepository = wineRepository;
        }

        // GET: api/<WineController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Wine>> Get([FromQuery] string? manufacturer = null, int year = 0, int price = 0, decimal rating = 0 )
        {
            IEnumerable<Wine> wines = _wineRepository.GetAll(manufacturer, year, price, rating);
            if(wines == null)
            {
                return BadRequest("Wines collection is null");
            }
            else if(!wines.Any())
            {
                return NoContent();
            }
            else
            {
                return Ok(wines);
            }

        }

        // GET api/<WineController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetById(int id)
        {
            Wine wine = _wineRepository.GetById(id);
            if(wine == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(wine);
            }
        }

        // POST api/<WineController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Wine> Post([FromBody] Wine value)
        {
            Wine wine = _wineRepository.Add(value);
            return CreatedAtAction(nameof(GetById), new { id = wine.Id }, wine);
        }

        // PUT api/<WineController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Wine> Put(int id, [FromBody] Wine value)
        {
            Wine wine = _wineRepository.Update(id, value);
            if(wine == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(wine);
            }
        }

        // DELETE api/<WineController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Wine> Delete(int id)
        {
            Wine wine = _wineRepository.Delete(id);
            if(wine == null)
            {
                return NotFound();
            }
            return Ok(wine);
        }
    }
}
