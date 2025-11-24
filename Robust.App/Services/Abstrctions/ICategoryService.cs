using Robust.DTO.Category;
using Robust.DTO.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robust.App.Services.Abstrctions
{
    public interface ICategoryService
    {
        public Task<ResultView<CategoryDTO>> CreateAsync(CategoryDTO entity);
        public Task<ResultView<CategoryDTO>> UpdateAsync(CategoryDTO entity);
        public Task<Pagintion<GetCategoryDTO>> GetAllAsync(int PageNumber, int PageSize);
        public Task<bool> DeleteAsync(int Id);
        public Task<GetCategoryDTO> GetOneAsync(int id);
    }
}
