﻿using CleanArchMvc.Application.DTOs;

namespace CleanArchMvc.Application.Interfaces
{
	public interface ICategoryService
	{
		Task<IEnumerable<CategoryDTO>> GetCategoriesAsync();
		Task<CategoryDTO> GetByIdAsync(int? id);
		Task CreateAsync(CategoryDTO category);
		Task UpdateAsync(CategoryDTO category);
		Task RemoveAsync(int id);
	}
}
