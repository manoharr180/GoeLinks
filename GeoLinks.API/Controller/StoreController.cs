using Amazon.Runtime.Internal.Util;
using GeoLinks.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace GeoLinks.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoreController : AppControllerBase
    {
        private readonly IStoreService storeService;
        private ILogger<StoreController> _logger;

        public StoreController(IStoreService storeService, ILogger<StoreController> logger)
        {
            this.storeService = storeService;
            this._logger = logger;
        }

        // GET: api/store
        [HttpGet]
        public async Task<IActionResult> GetAllStores()
        {
            _logger.LogInformation("Request started to get all stores.");
            var stores = await storeService.GetAllStoresAsync();
            return Ok(stores);
        }
    }
}