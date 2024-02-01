using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchMvc.Infra.Data.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly ApplicationDbContext Context;

		public ProductRepository(ApplicationDbContext context)
		{
			Context = context;
		}
		public async Task<Product> CreateAsync(Product product)
		{
			Context.Products.Add(product);
			await Context.SaveChangesAsync();
			return product;
		}

		public async Task<Product> GetByIdAsync(int? id)
		{
            return await Context.Products
                        .Include(c => c.Category)
                        .SingleOrDefaultAsync(p => p.Id == id);
        }

		//public async Task<Product> GetProductCategorieAsync(int id)
		//{
		//	return await Context.Products
		//				.Include(c => c.Category)
		//				.SingleOrDefaultAsync(p => p.Id == id);
		//}

		public async Task<IEnumerable<Product>> GetProductsAsync()
		{
            return await Context.Products.ToListAsync();
		}

		public async Task<Product> RemoveAsync(Product product)
		{
			Context.Products.Remove(product);
			await Context.SaveChangesAsync();
			return product;
		}

		public async Task<Product> UpdateAsync(Product product)
		{
			Context.Products.Update(product);
			await Context.SaveChangesAsync();
			return product;
		}
	}
}
