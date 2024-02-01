using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchMvc.Infra.Data.Repositories
{
	public class CategoryRepository : ICategoryRepository
	{
		private ApplicationDbContext Context;
		public CategoryRepository(ApplicationDbContext context) 
		{
			Context = context;
		}
		public async Task<Category> CreateAsync(Category category)
		{
			Context.Add(category);
			await Context.SaveChangesAsync();
			return category;
		}

		public async Task<Category> GetByIdAsync(int? id)
		{
			return await Context.Categories.FindAsync(id);
		}

		public async Task<IEnumerable<Category>> GetCategoriesAsync()
		{
			return await Context.Categories.ToListAsync();
		}

		public async Task<Category> RemoveAsync(Category category)
		{
			Context.Remove(category);
			await Context.SaveChangesAsync();
			return category;
		}

		public async Task<Category> UpdateAsync(Category category)
		{
			Context.Update(category);
			await Context.SaveChangesAsync();
			return category;
		}
	}
}
