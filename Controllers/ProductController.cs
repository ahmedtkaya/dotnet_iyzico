using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using DOTNET_iyzico.Repository;
using DOTNET_iyzico.Interfaces;
using DOTNET_iyzico.Models;
using DOTNET_iyzico.Dtos.Product;
using DOTNET_iyzico.Services;
using DOTNET_iyzico.Helpers;

namespace DOTNET_iyzico.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpPost]

        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto createProductDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _productRepository.CreateProductAsync(createProductDto);
            return Ok("Product created successfully.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var products = await _productRepository.GetAllAsync();
            return Ok(products);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productModel = await _productRepository.DeleteAsync(id);
            if (productModel == null)
            {
                return NotFound("Product does not exist");
            }
            return Ok(productModel);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}