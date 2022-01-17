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
        //_________________________________________ Initialize ______________________________________________\\
        private IBL _bl;
        private IMemoryCache _memoryCache; //put in Ilogger
        private ILogger _logger;

        public UserController(IBL bl, IMemoryCache memoryCache)
        {
            _bl = bl;
            _memoryCache = memoryCache;
        }


        //---------------------------------------------------------------------------------------------------\\
        // GET: UserController
        public List<Customers> Get()//Get All
        {
            List<Customers> allCustomers;// = _bl.GetAllStores();
            if (!_memoryCache.TryGetValue("Customers", out allCustomers))//null ref
            {
                allCustomers = _bl.GetAllCustomers();
                _memoryCache.Set("customer", allCustomers, new TimeSpan(0, 0, 30));
            }
            return allCustomers;
        }
       
        //---------------------------------------------------------------------------------------------------\\
        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //---------------------------------------------------------------------------------------------------\\
        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        //---------------------------------------------------------------------------------------------------\\
        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //---------------------------------------------------------------------------------------------------\\
        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //---------------------------------------------------------------------------------------------------\\
        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //---------------------------------------------------------------------------------------------------\\
        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //---------------------------------------------------------------------------------------------------\\
        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
