using GeoLinks.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GeoLinks.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoreController : ControllerBase
    {
        // Mock data for demonstration purposes
        private IStoreService storeService;
        public StoreController(IStoreService storeService)
        {
            this.storeService = storeService;
        }

        // GET: api/store
        [HttpGet]
        public IActionResult GetAllStores()
        {
            var stores = this.storeService.GetAllStoresAsync().Result;
            return Ok(JsonConvert.SerializeObject(stores));
        }

        // GET: api/store/{id}
        // [HttpGet("{id}")]
        // public IActionResult GetStoreById(int id)
        // {
        //     if (id < 0 || id >= Stores.Count)
        //     {
        //         return NotFound("Store not found.");
        //     }

        //     return Ok(Stores[id]);
        // }

        // // POST: api/store
        // [HttpPost]
        // public IActionResult CreateStore([FromBody] string storeName)
        // {
        //     if (string.IsNullOrWhiteSpace(storeName))
        //     {
        //         return BadRequest("Store name cannot be empty.");
        //     }

        //     Stores.Add(storeName);
        //     return CreatedAtAction(nameof(GetStoreById), new { id = Stores.Count - 1 }, storeName);
        // }

        // // PUT: api/store/{id}
        // [HttpPut("{id}")]
        // public IActionResult UpdateStore(int id, [FromBody] string storeName)
        // {
        //     if (id < 0 || id >= Stores.Count)
        //     {
        //         return NotFound("Store not found.");
        //     }

        //     if (string.IsNullOrWhiteSpace(storeName))
        //     {
        //         return BadRequest("Store name cannot be empty.");
        //     }

        //     Stores[id] = storeName;
        //     return NoContent();
        // }

        // // DELETE: api/store/{id}
        // [HttpDelete("{id}")]
        // public IActionResult DeleteStore(int id)
        // {
        //     if (id < 0 || id >= Stores.Count)
        //     {
        //         return NotFound("Store not found.");
        //     }

        //     Stores.RemoveAt(id);
        //     return NoContent();
        // }
    }
}