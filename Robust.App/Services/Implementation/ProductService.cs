using AutoMapper;
using Robust.App.Contracts;
using Robust.App.Services.Abstrctions;
using Robust.DTO.Products;
using Robust.DTO.Shared;
using Robust.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robust.App.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo productRepo;
        private readonly ICategoryRepo categoryRepo;
        private readonly IMapper mapper;
        public ProductService(IProductRepo _productRepo,IMapper _mapper,ICategoryRepo _categoryRepo)
        {
            productRepo = _productRepo;
            categoryRepo = _categoryRepo;
            mapper = _mapper;
        }
        public async Task<ResultView<ProductDTO>> CreateAsync(ProductDTO entity)
        {
            ResultView<ProductDTO> result = new();
            try
            {
                var category = await categoryRepo.GetOneAsync(entity.CategoryId);
                if (category == null)
                {
                    return new ResultView<ProductDTO>
                    {
                        ISSuccess = false,
                        Message = "Invalid CategoryId — Category does not exist",
                        Entity = null
                    };
                }
                var product = (await productRepo.GetAllAsync(p => p.Name == entity.Name)).FirstOrDefault();
                if (product != null)
                {
                    if (product.IsActive == false)
                    {
                        product.IsActive = true;
                        product.UpdatedDate = DateTime.Now;
                        await productRepo.UpdateAsync(product);
                        await productRepo.SaveChangesAsync();
                        var ReturnedPro = mapper.Map<ProductDTO>(product);
                        result = new()
                        {
                            Entity = ReturnedPro,
                            ISSuccess = true,
                            Message = "Product Restored Successfully"
                        };
                        return result;
                    }
                    else
                    {
                        result = new()
                        {
                            Entity = null,
                            ISSuccess = false,
                            Message = "This Product Exists"
                        };
                        return result;
                    }
                }
                var newOne = mapper.Map<Product>(entity);
                await productRepo.CreateAsync(newOne);
                await productRepo.SaveChangesAsync();
                result = new()
                {
                    Message = "Created Successfully",
                    ISSuccess = true,
                    Entity = entity
                };
                return result;
            }
            catch(Exception ex)
            {
                result = new()
                {
                    Message = "Error "+ex,
                    ISSuccess = false,
                    Entity = null
                };
                return result;
            }
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            var product = await productRepo.GetOneAsync(Id);
            if (product == null)
                return false;
            product.IsActive = false;
            await productRepo.UpdateAsync(product);
            return await productRepo.SaveChangesAsync() > 0;
        }

        public async Task<Pagintion<GetProductDTO>> GetAllAsync(int PageNumber, int PageSize)
        {
            var Count = (await productRepo.GetAllAsync(p => p.IsActive == true)).Count();
            var products = (await productRepo.GetAllAsync(p => p.IsActive == true)).OrderBy(p=>p.CreatedDate)
                .Skip(PageSize * (PageNumber - 1)).Take(PageSize).ToList();
            var tempData = mapper.Map<List<GetProductDTO>>(products);
            var data = new Pagintion<GetProductDTO>()
            {
                Count = Count,
                Data = tempData,
                PageSize = PageSize,
                CurrentPage = PageNumber
            };
            return data;
        }

        public async Task<GetProductDTO> GetOneAsync(int id)
        {
            var product = await productRepo.GetOneAsync(id);
            return mapper.Map<GetProductDTO>(product);
        }

        public async Task<ResultView<ProductDTO>> UpdateAsync(ProductDTO entity)
        {
            ResultView<ProductDTO> result = new ResultView<ProductDTO>();
            try
            {
                var existing = await productRepo.GetOneAsync(entity.Id);

                if (existing == null || existing.IsActive == false)
                {
                    result = new()
                    {
                        Entity = null,
                        ISSuccess = false,
                        Message = "Product Not Found or Deleted"
                    };
                    return result;
                }   
               
                mapper.Map(entity, existing);
                await productRepo.UpdateAsync(existing);
                await productRepo.SaveChangesAsync();

                var updatedProduct = await productRepo.GetOneAsync(existing.Id);
                var pro = mapper.Map<ProductDTO>(updatedProduct);
                result = new()
                {
                    Entity = pro,
                    ISSuccess = true,
                    Message = "Updated Successfully"
                };
                return result;

            }
            catch (Exception ex)
            {
                result = new()
                {
                    Entity = null,
                    ISSuccess = false,
                    Message = "Error " + ex
                };
                return result;
            }
        }
    }
}
