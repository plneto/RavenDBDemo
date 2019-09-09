namespace RavenDbDemo.Api.Indexes
{
    using System.Linq;
    using Domain;
    using Raven.Client.Documents.Indexes;

    public class Products_ByName : AbstractIndexCreationTask<Product>
    {
        public Products_ByName()
        {
            Map = products =>
                from product in products
                select new
                {
                    Name = product.Name
                };
        }
    }
}