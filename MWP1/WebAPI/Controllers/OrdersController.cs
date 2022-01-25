using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
using Models;
using BL;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Authorization;
using Serilog;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        //===================================================() Initialize ()===================================================\\
        private IBL _bl;
        private IMemoryCache _memoryCache; //put in Ilogger
        //private ILogger _logger;

        public OrdersController(IBL bl, IMemoryCache memoryCache)
        {
            _bl = bl;
            _memoryCache = memoryCache;
        }

        //------------------------------------------------<> METHOD <>---------------------------------------------------\\
        // GET: api/<OrdersController>
        [HttpGet]
        public List<Orders> Get()//Get All
        {
            List<Orders> allOrders;// = _bl.GetAllStores();
            if (!_memoryCache.TryGetValue("Orders", out allOrders))//null ref
            {
                allOrders = _bl.GetAllOrders();
                _memoryCache.Set("orders", allOrders, new TimeSpan(0, 0, 30));
            }
            return allOrders;
        }

        //------------------------------------------------<> METHOD <>---------------------------------------------------\\
        // GET: api/<OrdersController>
        [HttpGet("{storeId}")]
        public List<Orders> Get(int storeId)//Get All
        {
            List<Orders> allOrders;// = _bl.GetAllStores();
            if (!_memoryCache.TryGetValue("Orders", out allOrders))//null ref
            {
                allOrders = _bl.GetAllOrderByStore(storeId);
                _memoryCache.Set("orders", allOrders, new TimeSpan(0, 0, 30));
            }
            return allOrders;
        }

        /*
        //------------------------------------------------<> GetOrderByIdAsync <>---------------------------------------------------\\
        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Orders>> GetAsync(int id)
        {
            Orders foundOrder = await _bl.GetOrderByIdAsync(id);
            if (foundOrder.OrderId != 0)
            {
                return Ok(foundOrder);
            }
            else
            {
                return NoContent();
            }
        }
        */

        //------------------------------------------------<> AddOrder <>---------------------------------------------------\\
        // POST api/<OrdersController>
        [HttpPost]
        public ActionResult<Orders> Post([FromBody] Orders orderToAdd)
        {
            try
            {
                _bl.AddOrder(orderToAdd);
                //Created 201
                return Created("Order added!", orderToAdd);
            }
            catch (Exception ex)
            {
                //Dupelicate is 409, but I can't test that yet, don't know how
                return Conflict(ex.Message);
            }
        }

        //------------------------------------------------<> FinalizeOrder <>---------------------------------------------------\\
        // PUT api/<OrdersController>/5
        [HttpPost("{id}")]
        //public void Put(int id, [FromBody] int value)
        public void Post(int orderId)
        {
            _bl.PlaceOrder(orderId);
            //Created 201
        }


        //------------------------------------------------<> FinalizeOrder <>---------------------------------------------------\\
        // PUT api/<OrdersController>/5
        [HttpPut("{id}")]
        //public void Put(int id, [FromBody] int value)
        public ActionResult Put([FromBody] Orders changeOrdersInfo)
        {
            try
            {
                _bl.FinalizeOrder(changeOrdersInfo);
                //Created 201
                return Created("Orders finalized", changeOrdersInfo);
            }
            catch (Exception ex)
            {
                //Dupelicate is 409, but I can't test that yet, don't know how
                return Conflict(ex.Message);
            }

        }

        //------------------------------------------------<> DeleteOrders <>---------------------------------------------------\\
        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int table, int id)
        {
            Orders selectOrder = await _bl.GetOrderByIdAsync(id);
            if (selectOrder.OrderId == 0)
            {
                return NoContent();
            }
            //_bl.DeleteOrders(id);
            _bl.OmniDelete(4, id);
            Serilog.Log.Information("An Order mas deleted!");
            return Ok();
        }
    }
}
