using CleanArchMvc.Application.DTOs;

namespace CleanArchMvc.Application.Interfaces
{
    public interface IProductService
	{
		Task<IEnumerable<ProductDTO>> GetProductsAsync();
		///Task<ProductDTO> GetProductCategorieAsync(int id);
		Task<ProductDTO> GetByIdAsync(int? id);
		Task CreateAsync(ProductDTO product);
		Task UpdateAsync(ProductDTO product);
		Task RemoveAsync(int id);
	}
}
