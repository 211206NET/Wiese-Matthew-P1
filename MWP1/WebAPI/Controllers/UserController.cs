using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using BL;
using Microsoft.Extensions.Caching.Memory;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        //===================================================() Initialize ()===================================================\\
        private IBL _bl;
        private IMemoryCache _memoryCacheC; //put in Ilogger
        private ILogger _logger;

        public UserController(IBL bl, IMemoryCache memoryCache)
        {
            _bl = bl;
            _memoryCacheC = memoryCache;
        }


        //-----------------------------------------------<> GetAllCustomers <>--------------------------------------------------\\
        // GET: UserController
        [HttpGet]
        public List<Customers> Get()//Get All
        {
            List<Customers> allCustomers;// = _bl.GetAllStores();
            if (!_memoryCacheC.TryGetValue("Customers", out allCustomers))//null ref
            {
                allCustomers = _bl.GetAllCustomers();
                _memoryCacheC.Set("customer", allCustomers, new TimeSpan(0, 0, 30));
            }
            return allCustomers;
        }


        //--------------------------------------------<> GetCustomerByIdAsync <>-----------------------------------------------\\
        // GET api/<StoreController>/5 Get value or something abse don id e.g 5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customers>> GetAsync(int id)
        {
            Customers foundCustomer = await _bl.GetCustomerByIdAsync(id);
            if (foundCustomer.CustNumb != 0)
            {
                return Ok(foundCustomer);
            }
            else
            {
                return NoContent();
            }
        }

        //------------------------------------------------<> AddCustomer <>---------------------------------------------------\\
        //public void AddCustomer(Customers addCust)
        [HttpPost]
        public ActionResult<Customers> Post([FromBody] Customers addCust)
        {
            try
            {
                _bl.AddCustomer(addCust);
                //Created 201
                return Created("Good", addCust);
            }
            catch (Exception ex)
            {
                //Duplicate is 409, but I can't test that yet, don't know how
                return Conflict(ex.Message);
            }
        }

        //-------------------------------------------------<> ChangeUserInfo <>--------------------------------------------------\\
        // PUT api/<StoreController>/5  add to something
        //[Authorize]
        [HttpPut("{id}")]
        //public void Put(int id, [FromBody] int value)
        public ActionResult Put([FromBody] Customers changeCustomerInfo)
        {
            try
            {
                _bl.ChangeUserInfo(changeCustomerInfo);
                //Created 201
                return Created("Store updated", changeCustomerInfo);
            }
            catch (Exception ex)
            {
                //Dupelicate is 409, but I can't test that yet, don't know how
                return Conflict(ex.Message);
            }

        }

        //------------------------------------------------<> RemoveCustomer <>---------------------------------------------------\\
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
            _bl.OmniDelete(5, id);
            //return Ok();
        }

    }
}
