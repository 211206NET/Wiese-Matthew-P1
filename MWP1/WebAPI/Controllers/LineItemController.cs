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
    public class LineItemController : ControllerBase
    {
        //===================================================() Initialize ()===================================================\\
        private IBL _bl;
        private IMemoryCache _memoryCache; //put in Ilogger
        //private ILogger _logger;

        public LineItemController(IBL bl, IMemoryCache memoryCache)
        {
            _bl = bl;
            _memoryCache = memoryCache;
        }

        //------------------------------------------------<> GetAllLineItem <>---------------------------------------------------\\
        // GET: api/<LineItemController>
        [HttpGet]
        public List<LineItems> Get()//Get All
        {
            List<LineItems> allLineitems;// = _bl.GetAllStores();
            if (!_memoryCache.TryGetValue("LineItems", out allLineitems))//null ref
            {
                allLineitems = _bl.GetAllLineItem();
                _memoryCache.Set("lineItems", allLineitems, new TimeSpan(0, 0, 30));
            }
            return allLineitems;
        }

        //------------------------------------------------<> GetLineitemsByIdAsync <>---------------------------------------------------\\
        // GET api/<LineItemController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LineItems>> GetAsync(int id)
        {
            LineItems foundLineItems = await _bl.GetLineitemsByIdAsync(id);
            if (foundLineItems.Id != 0)
            {
                return Ok(foundLineItems);
            }
            else
            {
                return NoContent();
            }
        }

        //------------------------------------------------<> AddLineItem <>---------------------------------------------------\\
        // POST api/<LineItemController>
        [HttpPost]
        public ActionResult<LineItems> Post([FromBody] LineItems lineItemsToAdd)
        {
            try
            {
                _bl.AddLineItem(lineItemsToAdd);
                //Created 201
                return Created("LineItems added!", lineItemsToAdd);
            }
            catch (Exception ex)
            {
                //Dupelicate is 409, but I can't test that yet, don't know how
                return Conflict(ex.Message);
            }
        }


        //------------------------------------------------<> FinalizeLineItem <>---------------------------------------------------\\
        // PUT api/<LineItemController>/5
        //[Authorize]
        [HttpPut("{id}")]
        //public void Put(int id, [FromBody] int value)
        public ActionResult Put([FromBody] LineItems changeLineItemsInfo)
        {
            try
            {
                _bl.FinalizeLineItem(changeLineItemsInfo);
                //Created 201
                return Created("Line item finalized", changeLineItemsInfo);
            }
            catch (Exception ex)
            {
                //Dupelicate is 409, but I can't test that yet, don't know how
                return Conflict(ex.Message);
            }

        }

        //------------------------------------------------<> RemoveLineItem <>---------------------------------------------------\\
        // DELETE api/<LineItemController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int table, int id)
        {
            LineItems selectLineItems = await _bl.GetLineitemsByIdAsync(id);
            if (selectLineItems.Id == 0)
            {
                return NoContent();
            }
            //_bl.RemoveLineItem(id);
            _bl.OmniDelete(1, id);
            Serilog.Log.Information("An Lineitems mas deleted!");
            return Ok();
        }
    }
}
