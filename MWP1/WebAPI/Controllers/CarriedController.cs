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
            if (!_memoryCache.TryGetValue("ProdDetails", out allCarried))//null ref sl,jkhfnsjkhfgjk
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

        //------------------------------------------------<> AddCarried <>---------------------------------------------------\\
        // POST api/<CarriedController>
        [HttpPost]
        public ActionResult<ProdDetails> Post([FromBody] ProdDetails carriedToAdd)
        {
            try
            {
                _bl.AddCarried(carriedToAdd);
                //Created 201
                return Created("Product added to list of carried items", 201);
            }
            catch (Exception ex)
            {
                //Dupelicate is 409, but I can't test that yet, don't know how
                return Conflict(ex.Message);
            }
        }

        //------------------------------------------------<> ChangeCarried <>---------------------------------------------------\\
        // PUT api/<CarriedController>/5
        [HttpPut("{id}")]
        //public void Put(int id, [FromBody] int value)
        public ActionResult Put([FromBody] ProdDetails changeProdDetailsInfo)
        {
            try
            {
                _bl.ChangeCarried(changeProdDetailsInfo);
                //Created 201
                return Created("Product updated", changeProdDetailsInfo);
            }
            catch (Exception ex)
            {
                //Dupelicate is 409, but I can't test that yet, don't know how
                return Conflict(ex.Message);
            }

        }

        //------------------------------------------------<> RemoveItem <>---------------------------------------------------\\
        // DELETE api/<CarriedController>/5
        [HttpDelete("{id}")]
        public void Delete(int table, int id)
        {
            //ProdDetails selectCarried = _bl.RemoveItem(id);
            //if (selectCarried.APN == null)
            //{
                //return NoContent();
            //}
            //_bl.RemoveItem(id);
            _bl.OmniDelete(3, id);
            //return Ok();
        }
    }
}
