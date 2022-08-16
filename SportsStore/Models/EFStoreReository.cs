using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class EFStoreReository:IStoreRepository
    {
        private StoreDbContext context;
        public EFStoreReository(StoreDbContext context)
        {
            this.context = context;
        }
        public IQueryable<Product> Products => context.Products;
    }
}
