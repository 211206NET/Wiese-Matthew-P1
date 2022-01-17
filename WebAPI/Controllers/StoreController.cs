using Microsoft.AspNetCore.Mvc;
using Models;
using BL;
using Microsoft.Extensions.Caching.Memory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
//Need CRUD
namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        //_________________________________________ Initialize ______________________________________________\\
        private IBL _bl;
        private IMemoryCache _memoryCache; //put in Ilogger
        private ILogger _logger;

        public StoreController(IBL bl, IMemoryCache memoryCache)
        {
            _bl = bl;
            _memoryCache = memoryCache;
        }

        //---------------------------------------------------------------------------------------------------\\
        // GET: api/<StoreController>  get a list
        [HttpGet]
        public List<Store> Get()//Get All
        {
            List<Store> allStores;// = _bl.GetAllStores();
            if (!_memoryCache.TryGetValue("Store", out allStores))//null ref
            {
                allStores = _bl.GetAllStores();
                _memoryCache.Set("store", allStores, new TimeSpan(0, 0, 30));
            }
            return allStores;
        }

        //---------------------------------------------------------------------------------------------------\\
        // GET api/<StoreController>/5 Get value or something abse don id e.g 5
        [HttpGet("{id}")]
        public async Task<ActionResult<Store>> GetAsync(int id)
        {
            Store foundStore = await _bl.GetStoreByIdAsync(id);
            if (foundStore.StoreID != 0)
            {
                return Ok(foundStore);
            }
            else
            {
                return NoContent();
            }
        }

        //---------------------------------------------------------------------------------------------------\\
        // POST api/<StoreController> Upload
        [HttpPost]
        public ActionResult<Store> Post([FromBody] Store storeToAdd)
        {
            try
            {
                _bl.AddStore(storeToAdd);
                //Created 201
                return Created("Good",storeToAdd);
            }
            catch(Exception ex)
            {
                //Dupelicate is 409, but I can't test that yet, don't know how
                return Conflict(ex.Message);
            }
        }

        //---------------------------------------------------------------------------------------------------\\
        // PUT api/<StoreController>/5  add to something
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
                
        }

        //---------------------------------------------------------------------------------------------------\\
        // DELETE api/<StoreController>/5  delete the thing
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
