using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CleanArchMvc.WebUI.Controllers
{    
    public class ProductsController : Controller
    {
        private readonly IProductService ProductService;
        private readonly ICategoryService CategoryService;
        private readonly IWebHostEnvironment _environment;
        public ProductsController(IProductService productService,
                                  ICategoryService categoryService,
                                  IWebHostEnvironment environment)
        {
            ProductService = productService;
            CategoryService = categoryService;
            _environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await ProductService.GetProductsAsync();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories =
                new SelectList(await CategoryService.GetCategoriesAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO productDto)
        {
            if (ModelState.IsValid)
            {
                await ProductService.CreateAsync(productDto);
                return RedirectToAction(nameof(Index));
            }
            return View(productDto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var productDto = await ProductService.GetByIdAsync(id);
            if (productDto == null) return NotFound();
            ViewBag.Categories =
                new SelectList(await CategoryService.GetCategoriesAsync(), "Id", "Name");
            return View(productDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductDTO productDto)
        {
            if (ModelState.IsValid)
            {
                await ProductService.UpdateAsync(productDto);
                return RedirectToAction(nameof(Index));
            }
            return View(productDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var productDto = await ProductService.GetByIdAsync(id);
            if(productDto==null) return NotFound();
            return View(productDto);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await ProductService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var productDto = await ProductService.GetByIdAsync(id);

            if (productDto == null) return NotFound();
            var wwwroot = _environment.WebRootPath;
            var image = Path.Combine(wwwroot, "images\\" + productDto.Image);
            var exists = System.IO.File.Exists(image);
            ViewBag.ImageExist = exists;

            return View(productDto);
        }

    }
}