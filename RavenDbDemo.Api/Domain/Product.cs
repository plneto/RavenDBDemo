﻿namespace RavenDbDemo.Api.Domain
{
    public class Product
    {
        public string Id { get; private set; }

        public string Name { get; private set; }

        public decimal Price { get; private set; }

        public decimal Discount { get; private set; }

        public int QtyInStock { get; private set; }

        public Product(string id, string name, decimal price, decimal discount, int qtyInStock)
        {
            Id = id;
            Name = name;
            Price = price;
            Discount = discount;
            QtyInStock = qtyInStock;
        }
    }
}