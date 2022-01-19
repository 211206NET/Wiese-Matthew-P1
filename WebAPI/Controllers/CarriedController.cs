using Microsoft.AspNetCore.Mvc;
using Models;
using BL;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarriedController : ControllerBase
    {
        //===================================================() Initialize ()===================================================\\
        private IBL _bl;
        private IMemoryCache _memoryCache; //put in Ilogger
        private ILogger _logger;

        public CarriedController(IBL bl, IMemoryCache memoryCache)
        {
            _bl = bl;
            _memoryCache = memoryCache;
        }

        //------------------------------------------------<> GetAllCarried <>---------------------------------------------------\\
        // GET: api/<CarriedController>
        [HttpGet]
        public List<ProdDetails> Get()//Get All
        {
            List<ProdDetails> allCarried;// = _bl.GetAllStores();
            if (!_memoryCache.TryGetValue("ProdDetails", out allCarried))//null ref
            {
                allCarried = _bl.GetAllCarried();
                _memoryCache.Set("prodDetails", allCarried, new TimeSpan(0, 0, 30));
            }
            return allCarried;
        } 
        
        //------------------------------------------<> GetCarriedByIdAsync <>---------------------------------------------\\
        // GET api/<CarriedController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProdDetails>> GetAsync(int id)
        {
            ProdDetails foundCarried = await _bl.GetCarriedByIdAsync(id);
            if (foundCarried.APN != 0)
            {
                return Ok(foundCarried);
            }
            else
            {
                return NoContent();
            }
        }

        //------------------------------------------------<> METHOD <>---------------------------------------------------\\
        // POST api/<CarriedController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        //------------------------------------------------<> METHOD <>---------------------------------------------------\\
        // PUT api/<CarriedController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        //------------------------------------------------<> METHOD <>---------------------------------------------------\\
        // DELETE api/<CarriedController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
