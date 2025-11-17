using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robust.DTO.Products;
using Robust.DTO.Shared;

namespace Robust.App.Services.Abstrctions
{
    public interface IProductService
    {
        public Task<ResultView<ProductDTO>> CreateAsync(ProductDTO entity);
        public Task<ResultView<ProductDTO>> UpdateAsync(ProductDTO entity);
        public Task<Pagintion<GetProductDTO>> GetAllAsync(int PageNumber, int PageSize);
        public Task<bool> DeleteAsync(int Id);
        public Task<GetProductDTO> GetOneAsync(int id);
    }
}
