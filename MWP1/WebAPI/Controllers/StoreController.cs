using Microsoft.AspNetCore.Mvc;
using Models;
using BL;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
//Need CRUD
namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        //===================================================() Initialize ()===================================================\\
        private IBL _bl;
        private IMemoryCache _memoryCache; //put in Ilogger
        private ILogger _logger;

        public StoreController(IBL bl, IMemoryCache memoryCache)
        {
            _bl = bl;
            _memoryCache = memoryCache;
        }

        //------------------------------------------------<> GetAllStores <>---------------------------------------------------\\
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

        //---------------------------------------------<> GetStoreByIdAsync <>--------------------------------------------------\\
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

        //------------------------------------------------<> AddStore <>-------------------------------------------------------\\
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

        //-------------------------------------------------<> ChangeStoreInfo <>--------------------------------------------------\\
        // PUT api/<StoreController>/5  add to something
        //[Authorize]
        [HttpPut("{id}")]
        //public void Put(int id, [FromBody] int value)
        public ActionResult Put([FromBody] Store changeStoreInfo)
        {
            try
            {
                _bl.ChangeStoreInfo(changeStoreInfo);
                //Created 201
                return Created("Product updated", changeStoreInfo);
            }
            catch (Exception ex)
            {
                //Dupelicate is 409, but I can't test that yet, don't know how
                return Conflict(ex.Message);
            }

        }

        //-------------------------------------------------<> RemoveStore <>--------------------------------------------------\\
        // DELETE api/<StoreController>/5  delete the thing, Cannot delete store until FKs are clear first
        [HttpDelete("{id}")]
        public async void Delete(int id)
        {
            Store selectStore = await _bl.GetStoreByIdAsync(id);
            if (selectStore.StoreID == null)
            {
                //return NoContent();
            }
            _bl.RemoveStore(id);
            //return Ok();
        }
    }
}
