using Microsoft.AspNetCore.Mvc;
using Robust.App.Services.Abstrctions;
using Robust.DTO.Category;
using Robust.Models;

namespace Robust.API.Controllers
{
    [Route("api/Category")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        public CategoryController(ICategoryService _categoryService)
        {
            categoryService = _categoryService;
        }
     
        [HttpPost("CreateCategory")]
        public async Task<IActionResult> Create(CategoryDTO categoryDTO)
        {
            if (ModelState.IsValid)
            {
                var category = await categoryService.CreateAsync(categoryDTO);
                return Ok(new { Message = category.Message });
            }
            return BadRequest(new { Message = "Error in Validations" });
        }
        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> Update(CategoryDTO categoryDTO)
        {
            if (ModelState.IsValid)
            {
                var category = await categoryService.UpdateAsync(categoryDTO);
                return Ok(new { Message = category.Message });
            }
            return BadRequest(new { Message = "Error in Validations" });
        }
        [HttpGet("GetCategories")]
        public async Task<IActionResult> GetAll(int PageNumber = 1, int PageSize= 5)
        {
            var data = await categoryService.GetAllAsync(PageNumber, PageSize);
            return Ok(data);
        }
        [HttpDelete("DeleteCategory")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await categoryService.DeleteAsync(id);
            if (data)
                return Ok(new { Message = "Deleted Successfully" });
            return BadRequest(new { Messge = "Error" });
        }
    }
}
