using Microsoft.AspNetCore.Mvc;
using Robust.App.Services.Abstrctions;
using Robust.App.Services.Implementation;
using Robust.DTO.Products;

namespace Robust.API.Controllers
{
    [Route("api/Product")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        public ProductController(IProductService _productService)
        {
            productService = _productService;
        }
       
        [HttpPost("CreateProduct")]
        public async Task<IActionResult> Create(ProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                var product = await productService.CreateAsync(productDTO);
                return Ok(new { Message=product.Message });
            }
            return BadRequest(new { Message = "Error in Validations" });
        }
        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> Update(ProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                var product = await productService.UpdateAsync(productDTO);
                return Ok(new { Message = product.Message });
            }
            return BadRequest(new { Message = "Error in Validations" });
        }
        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetAll(int PageNumber = 1, int PageSize = 5)
        {
            var data = await productService.GetAllAsync(PageNumber, PageSize);
            return Ok(data);
        }
        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await productService.DeleteAsync(id);
            if (data)
                return Ok(new { Message = "Deleted Successfully" });
            return BadRequest(new { Messge = "Error" });
        }

    }
}
