namespace RavenDbDemo.Api.Indexes
{
    using System.Linq;
    using Domain;
    using Raven.Client.Documents.Indexes;

    public class Products_FinalPrice : AbstractIndexCreationTask<Product>
    {
        public class Result
        {
            public decimal FinalPrice { get; set; }
        }

        public Products_FinalPrice()
        {
            Map = products =>
                from product in products
                select new
                {
                    FinalPrice = product.Price - product.Discount
                };
        }
    }
}