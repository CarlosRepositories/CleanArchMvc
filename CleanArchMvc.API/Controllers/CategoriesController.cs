using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService CategoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            CategoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            var categories = await CategoryService.GetCategoriesAsync();

            if (categories == null)
            {
                return NotFound("Categories not found.");
            }
            return Ok(categories);
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            var category = await CategoryService.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound("Category not found.");
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> Create([FromBody] CategoryDTO categoryDTO)
        {
            if (categoryDTO == null)
                return BadRequest("Invalid data");
            await CategoryService.CreateAsync(categoryDTO);

            return new CreatedAtRouteResult("GetCategory", new {id = categoryDTO.Id}, categoryDTO);
        }

        [HttpPut]
        public async Task<ActionResult> Update(int id, [FromBody] CategoryDTO categoryDto)
        {
            if (id != categoryDto.Id)
                return BadRequest();

            if (categoryDto == null)
                return BadRequest("Invalid data");            

            await CategoryService.UpdateAsync(categoryDto);

            return Ok(categoryDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> Delete(int id)
        {
            var category = await CategoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound("Category not found");
            }

            await CategoryService.RemoveAsync(id);

            return Ok(category);

        }
    }
}
