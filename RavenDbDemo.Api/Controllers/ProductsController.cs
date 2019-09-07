using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Sparrow.Collections;

namespace RavenDbDemo.Api.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Domain;
    using Raven.Client.Documents;
    using Raven.Client.Documents.Session;

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IAsyncDocumentSession _asyncDocumentSession;

        public ProductsController(IAsyncDocumentSession asyncDocumentSession)
        {
            _asyncDocumentSession = asyncDocumentSession;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            var products = await _asyncDocumentSession.Query<Product>()
                .ToListAsync();

            return products;
        }

        [HttpGet("search/{searchTerm}")]
        public async Task<ActionResult<IEnumerable<Product>>> SearchByName(string searchTerm)
        {
            var products = await _asyncDocumentSession.Query<Product>()
                .Search(x => x.Name, searchTerm)
                .ToListAsync();

            return products;
        }

        [HttpPost("seed/{count}")]
        public async Task<ActionResult> Seed(int count)
        {
            for (var i = 0; i < count; i++)
            {
                var product = new Product(string.Empty, $"name_{i}", i * 5, i);

                await _asyncDocumentSession.StoreAsync(product);
            }

            await _asyncDocumentSession.SaveChangesAsync();

            return Ok();
        }
    }
}