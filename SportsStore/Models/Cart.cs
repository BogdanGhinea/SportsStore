using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models {
    public class Cart {
        public List<CartLine> Lines { get; set; } = new List<CartLine>();

        public virtual void AddItem(Product product, int quantity) {
            CartLine line = Lines
                            .Where(p => p.Product.ProductID == product.ProductID)
                            .FirstOrDefault();
            if (line == null) {
                Lines.Add(new CartLine() {
                    Product = product,
                    Quantity = quantity
                });
            }
            else {
                line.Quantity += quantity;
            }
        }
        public virtual void RemoveLine(Product product) {
            Lines.RemoveAll(l => l.Product.ProductID == product.ProductID);
        }
        public decimal ComputeTotalValue() {
            return Lines.Sum(l => l.Product.Price * l.Quantity);
        }
        public virtual void Clear() {
            Lines.Clear();
        }
        public virtual void SetQuantity(long productId, int quantity) {
            CartLine line = Lines
                            .First(cl => cl.Product.ProductID == productId);
            line.Quantity = quantity;
        }
    }
}
