using ShopApp.Core.Entities;
using ShopApp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApi.Data.Repositories
{
    public class ProductRepository:Repository<Product>,IProductRepository
    {
        private readonly ShopDbContext _context;

        public ProductRepository(ShopDbContext context):base(context)
        {
           
        }
    }
}
