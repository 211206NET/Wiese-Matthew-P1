using Microsoft.AspNetCore.Mvc;
using Models;
using BL;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Authorization;
using Serilog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        //===================================================() Initialize ()===================================================\\
        private IBL _bl;
        private IMemoryCache _memoryCache; //put in Ilogger
        //private ILogger _logger;

        public InventoryController(IBL bl, IMemoryCache memoryCache)
        {
            _bl = bl;
            _memoryCache = memoryCache;
        }

        //------------------------------------------------<> GetAllInventory <>---------------------------------------------------\\
        // GET: api/<InventoryController>
        [HttpGet]
        public List<Inventory> Get()
        {
            List<Inventory> allInventory;// = _bl.GetAllStores();
            if (!_memoryCache.TryGetValue("Inventory", out allInventory))//null ref
            {
                allInventory = _bl.GetAllInventory();
                _memoryCache.Set("inventory", allInventory, new TimeSpan(0, 0, 30));
            }
            return allInventory;
        }

        [HttpGet("{id}")]
        public List<Inventory> Get(int id)
        {
            List<Inventory> allStoreInv;// = _bl.GetAllStores();
            allStoreInv =  _bl.GetAllInventoryByStore(id);
            return allStoreInv;
        }
        /*
        //------------------------------------------------<> GetAllInventoryByStore <>---------------------------------------------------\\
        // GET: api/<InventoryController>
        [HttpGet("{id}")]
        public async Task<ActionResult<Inventory>> GetAsync(int storeId)
        {
            Inventory foundInventory = await _bl.GetAllInventoryByStoreAsync(storeId);
            if (foundInventory.Id != 0)
            {
                return Ok(foundInventory);
            }
            else
            {
                return NoContent();
            }
        }*/

        //------------------------------------------------<> AddInventory <>---------------------------------------------------\\
        // POST api/<InventoryController>
        [HttpPost]
        public ActionResult<Inventory> Post([FromBody] Inventory inventoryToAdd)
        {
            try
            {
                _bl.AddInventory(inventoryToAdd);
                //Created 201
                return Created("Inventory added", inventoryToAdd);
            }
            catch (Exception ex)
            {
                //Dupelicate is 409, but I can't test that yet, don't know how
                return Conflict(ex.Message);
            }
        }

        //------------------------------------------------<> ChangeInventory <>---------------------------------------------------\\
        // PUT api/<InventoryController>/5
        [HttpPut("{id}")]
        //public void Put(int id, [FromBody] int value)
        public ActionResult Put(int inventoryId, int qtyToChange)//[FromBody] 
        {
            try
            {
                _bl.ChangeInventory(inventoryId, qtyToChange);
                //Created 201
                return Created("Inventory updated", inventoryId);
            }
            catch (Exception ex)
            {
                //Dupelicate is 409, but I can't test that yet, don't know how
                return Conflict(ex.Message);
            }

        }

        //------------------------------------------------<> RemoveInventory <>---------------------------------------------------\\
        // DELETE api/<InventoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int table, int id)
        {
            //Inventory selectInventory = await _bl.GetStoreByIdAsync(id);
            //if (selectInventory.Id == null)
            //{
            //    //return NoContent();
            //}
            //_bl.RemoveInventory(id);
            _bl.OmniDelete(2, id);
            Serilog.Log.Information("An Inventory mas deleted!");
            //return Ok();
        }

    }
}
