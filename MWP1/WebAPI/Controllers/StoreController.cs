using Microsoft.AspNetCore.Mvc;
using Models;
using BL;
using CustomExceptions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Authorization;
using Serilog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
//Need CRUD
namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        //===================================================() Initialize ()===================================================\\
        private IStoreBL _bl;
        private IMemoryCache _memoryCache; //put in Ilogger
        //private ILogger _logger;
        //public string? Message { get; set; }

        public StoreController(IStoreBL bl, IMemoryCache memoryCache)//, ILogger<StoreController> logger)
        {
            _bl = bl;
            _memoryCache = memoryCache;
            //_logger = logger;
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

        //------------------------------------------------<> Search <>-------------------------------------------------------\\
        [HttpGet("search/{term}")]
        public List<Store> Search(string term)
        {
            return _bl.SearchStores(term);
        }

        //------------------------------------------------<> AddStore <>-------------------------------------------------------\\
        // POST api/<StoreController> Upload
        [HttpPost]
        public ActionResult<Store> Post([FromBody] Store storeToAdd)
        {
            try
            {
                _bl.AddStore(storeToAdd);
                //Message = $"Store made!";
                //_logger.LogInformation(Message);
                Serilog.Log.Information("A store was made!");
                return Created("Store added!",storeToAdd);
            }
            catch(DuplicateRecordException ex)//Doesn't catch, I used the duplicate method in DBRepo to catch it instead
            {
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
                return Created("Store updated", changeStoreInfo);
            }
            catch (Exception ex)
            {
                //Dupelicate is 409, but I can't test that yet, don't know how
                return Conflict(ex.Message);
            }

        }

        //-------------------------------------------------<> Delete <>--------------------------------------------------\\
        // DELETE api/<RestaurantController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            _bl.Delete(await _bl.GetStoreByIdAsync(id));
        }
        //-------------------------------------------------<> RemoveStore <>--------------------------------------------------\\
        // DELETE api/<StoreController>/5  delete the thing, Cannot delete store until FKs are clear first
        // [HttpDelete("{id}")]
        // public async Task<ActionResult> Delete(int table, int id)
        // {
        //     Store selectStore = await _bl.GetStoreByIdAsync(id);
        //     if (selectStore.StoreID == 0)
        //     {
        //         return NoContent();
        //     }
        //     //_bl.RemoveStore(id);
        //     _bl.OmniDelete(6,id);
        //     Serilog.Log.Information("A store was deleted!");
        //     return Ok();
        // }
    }
}
