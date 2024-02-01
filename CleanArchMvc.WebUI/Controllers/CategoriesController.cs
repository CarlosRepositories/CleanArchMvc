using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.WebUI.Controllers
{
	[Authorize]
	public class CategoriesController : Controller
	{
		private readonly ICategoryService CategoryService;
		public CategoriesController(ICategoryService categoryService)
		{
			CategoryService = categoryService;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var categories = await CategoryService.GetCategoriesAsync();
			return View(categories);
		}

		[HttpGet]
		public async Task<IActionResult> Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(CategoryDTO categoryDTO)
		{
			if (ModelState.IsValid)
			{
				try
				{
					await CategoryService.CreateAsync(categoryDTO);
				}
				catch (Exception ex)
				{
					throw;
				}

				return RedirectToAction("Index");
			}
			return View(categoryDTO);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null) return NotFound();
			var categoryDto = await CategoryService.GetByIdAsync(id);
			if (categoryDto == null) return NotFound();
			return View(categoryDto);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(CategoryDTO categoryDTO)
		{
			if (ModelState.IsValid)
			{
				try
				{
					await CategoryService.UpdateAsync(categoryDTO);
				}
				catch (Exception ex)
				{
					throw;
				}

				return RedirectToAction("Index");
			}
			return View(categoryDTO);
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null) return NotFound();
			var categoryDto = await CategoryService.GetByIdAsync(id);
			if (categoryDto == null) return NotFound();
			return View(categoryDto);
		}

		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			await CategoryService.RemoveAsync(id);
			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> Details(int? id)
		{
			if(id == null) return NotFound();
			var categoryDto = await CategoryService.GetByIdAsync(id);
			if (categoryDto == null) return NotFound(); 
			return View(categoryDto);
		}
	}
}
