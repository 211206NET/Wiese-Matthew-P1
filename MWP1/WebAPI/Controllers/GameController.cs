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
    public class GameController : ControllerBase
    {
        //===================================================() Initialize ()===================================================\\
        private IGameBL _bl;
        private IMemoryCache _memoryCache; //put in Ilogger
        //private ILogger _logger;
        //public string? Message { get; set; }

        public GameController(IGameBL bl, IMemoryCache memoryCache)//, ILogger<GameController> logger)
        {
            _bl = bl;
            _memoryCache = memoryCache;
            //_logger = logger;
        }

        //------------------------------------------------<> GetAllGames <>---------------------------------------------------\\
        // GET: api/<GameController>  get a list
        [HttpGet]
        public List<Game> Get()//Get All
        {
            List<Game> allGames;// = _bl.GetAllGames();
            if (!_memoryCache.TryGetValue("Game", out allGames))//null ref
            {
                allGames = _bl.GetAllGames();
                _memoryCache.Set("Game", allGames, new TimeSpan(0, 0, 30));
            }
            return allGames;
        }

        //---------------------------------------------<> GetGameByIdAsync <>--------------------------------------------------\\
        // GET api/<GameController>/5 Get value or something abse don id e.g 5    
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetAsync(int id)
        {
            Game foundGame = await _bl.GetGameByIdAsync(id);
            if (foundGame.GameID != 0)
            {
                return Ok(foundGame);
            }
            else
            {
                return NoContent();
            }
        }

        //------------------------------------------------<> AddGame <>-------------------------------------------------------\\
        // POST api/<GameController> Upload
        [HttpPost]
        public ActionResult<Game> Post([FromBody] Game GameToAdd)
        {
            try
            {
                _bl.AddGame(GameToAdd);
                //Message = $"Game made!";
                //_logger.LogInformation(Message);
                Serilog.Log.Information("A Game was made!");
                return Created("Game added!", GameToAdd);
            }
            catch (DuplicateRecordException ex)//Doesn't catch, I used the duplicate method in DBRepo to catch it instead
            {
                return Conflict(ex.Message);
            }
        }

        //-------------------------------------------------<> ChangeGameInfo <>--------------------------------------------------\\
        // PUT api/<GameController>/5  add to something
        //[Authorize]
        [HttpPut("{id}")]
        //public void Put(int id, [FromBody] int value)
        public ActionResult Put([FromBody] Game changeGameInfo)
        {
            try
            {
                _bl.ChangeGameInfo(changeGameInfo);
                //Created 201
                return Created("Game updated", changeGameInfo);
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
            _bl.Delete(await _bl.GetGameByIdAsync(id));
        }
        //-------------------------------------------------<> RemoveGame <>--------------------------------------------------\\
        // DELETE api/<GameController>/5  delete the thing, Cannot delete Game until FKs are clear first
        // [HttpDelete("{id}")]
        // public async Task<ActionResult> Delete(int table, int id)
        // {
        //     Game selectGame = await _bl.GetGameByIdAsync(id);
        //     if (selectGame.GameID == 0)
        //     {
        //         return NoContent();
        //     }
        //     //_bl.RemoveGame(id);
        //     _bl.OmniDelete(6,id);
        //     Serilog.Log.Information("A Game was deleted!");
        //     return Ok();
        // }
    }
}
