﻿using MDShop.Catalog.Dtos.ProductDtos;
using MDShop.Catalog.Services.ProductServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MDShop.Catalog.Controllers {
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService) {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> ProductList() {
            var values = await _productService.GetAllProductAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(string id) {
            var values = await _productService.GetByIdProductAsync(id);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto) {
            await _productService.CreateProductAsync(createProductDto);
            return Ok("Ürün başarıyla eklendi");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(string id) {
            await _productService.DeleteProductAsync(id);
            return Ok("Ürün başarıyla silindi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto) {
            await _productService.UpdateProductAsync(updateProductDto);
            return Ok("Ürün başarıyla güncellendi");
        }

        [HttpGet("ProductListWithCategory")]
        public async Task<IActionResult> ProductListWithCategory() {
            var values = await _productService.GetProductsWithCategoryAsync();
            return Ok(values);
        }

        [HttpGet("GetProductsWithCategoryByCatetegoryId/{id}")]
        public async Task<IActionResult> GetProductsWithCategoryByCatetegoryId(string id) {
            var values = await _productService.GetProductsWithCategoryByCategoryIdAsync(id);
            return Ok(values);
        }

    }
}
