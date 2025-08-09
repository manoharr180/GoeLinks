using GeoLinks.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GeoLinks.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoreController : AppControllerBase
    {
        private readonly IStoreService storeService;

        public StoreController(IStoreService storeService)
        {
            this.storeService = storeService;
        }

        // GET: api/store
        [HttpGet]
        public async Task<IActionResult> GetAllStores()
        {
            var stores = await storeService.GetAllStoresAsync();
            return Ok(stores);
        }
    }
}