using AutoMapper;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;

namespace CleanArchMvc.Application.Services
{
	public class CategoryService : ICategoryService
	{
        public ICategoryRepository CategoryRepository { get; set; }
        public IMapper Mapper { get; set; }

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            CategoryRepository = categoryRepository;
            Mapper = mapper;
        }

		public async Task<IEnumerable<DTOs.CategoryDTO>> GetCategoriesAsync()
		{
			var categoryEntities = await CategoryRepository.GetCategoriesAsync();
			return Mapper.Map<IEnumerable<DTOs.CategoryDTO>>(categoryEntities);
		}

		public async Task<DTOs.CategoryDTO> GetByIdAsync(int? id)
		{
			var category = await CategoryRepository.GetByIdAsync(id);
			return Mapper.Map<DTOs.CategoryDTO>(category);
		}

		public async Task CreateAsync(DTOs.CategoryDTO category)
		{
			var categoryEntity = Mapper.Map<Category>(category);
			await CategoryRepository.CreateAsync(categoryEntity);
		}

		public async Task UpdateAsync(DTOs.CategoryDTO category)
		{
			var categoryEntity = Mapper.Map<Category>(category);
			await CategoryRepository.UpdateAsync(categoryEntity);
		}

		public async Task RemoveAsync(int id)
		{
			var category = await CategoryRepository.GetByIdAsync(id);			
			await CategoryRepository.RemoveAsync(category);
		}
	}
}
