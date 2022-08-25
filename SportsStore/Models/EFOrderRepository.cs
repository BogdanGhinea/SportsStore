using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models {
    public class EFOrderRepository : IOrderRepository {
        private StoreDbContext context;
        public EFOrderRepository(StoreDbContext ctx) {
            context = ctx;
        }
        public IQueryable<Order> Orders {
            get {
                return context.Orders
                          .Include(o => o.Lines)
                          .ThenInclude(l => l.Product);
            }
        }
        public void SaveOrder(Order order) {
            context.AttachRange(order.Lines.Select(l => l.Product));
            if (order.OrderID == 0) {
                context.Orders.Add(order);
            }
            context.SaveChanges();
        }
    }
}
