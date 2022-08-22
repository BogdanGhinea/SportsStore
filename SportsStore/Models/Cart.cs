using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class Cart
    {
        private List<CartLine> Lines { get; set; } = new List<CartLine>();

    public void AddItem(Product product,int quantity)
        {
            CartLine line = Lines
                            .Where(p => p.Product.ProductId == product.ProductId)
                            .FirstOrDefault();
            if (line == null)
            {
                Lines.Add(new CartLine()
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public void RemoveLine(Product product)
        {
            Lines.RemoveAll(l => l.Product.ProductId == product.ProductId);
        }
        public decimal ComputeTotalValue()
        {
            return Lines.Sum(l => l.Product.Price * l.Quantity);
        }
        public void Clear()
        {
            Lines.Clear();
        }
    }

}
