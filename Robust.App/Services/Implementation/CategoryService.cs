using AutoMapper;
using Robust.App.Contracts;
using Robust.App.Services.Abstrctions;
using Robust.DTO.Category;
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
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryRepo categoryRepo;
        private readonly IMapper mapper;
        public CategoryService(ICategoryRepo _categoryRepo,IMapper _mapper)
        {
            categoryRepo = _categoryRepo;
            mapper = _mapper;
        }

        public async Task<ResultView<CategoryDTO>> CreateAsync(CategoryDTO entity)
        {
            ResultView<CategoryDTO> result = new();
            try
            {
                var category = (await categoryRepo.GetAllAsync(p => p.Name == entity.Name)).FirstOrDefault();
                if (category != null)
                {                   
                    result = new()
                    {
                        Entity = null,
                        ISSuccess = false,
                        Message = "This Category Exists"
                    };
                    return result;
                    
                }
                var newOne = mapper.Map<Category>(entity);
                await categoryRepo.CreateAsync(newOne);
                await categoryRepo.SaveChangesAsync();
                result = new()
                {
                    Message = "Created Successfully",
                    ISSuccess = true,
                    Entity = entity
                };
                return result;
            }
            catch (Exception ex)
            {
                result = new()
                {
                    Message = "Error " + ex,
                    ISSuccess = false,
                    Entity = null
                };
                return result;
            }
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            var category = await categoryRepo.GetOneAsync(Id);
            if (category == null)
                return false;
            await categoryRepo.DeleteAsync(category);
            return await categoryRepo.SaveChangesAsync() > 0;
        }

        public async Task<Pagintion<GetCategoryDTO>> GetAllAsync(int PageNumber, int PageSize)
        {
            var Count = (await categoryRepo.GetAllAsync()).Count();
            var categories = (await categoryRepo.GetAllAsync()).OrderBy(c=>c.CreatedDate)
                .Skip(PageSize * (PageNumber - 1)).Take(PageSize).ToList();
            var tempData = mapper.Map<List<GetCategoryDTO>>(categories);
            var data = new Pagintion<GetCategoryDTO>()
            {
                Count = Count,
                Data = tempData,
                PageSize = PageSize,
                CurrentPage = PageNumber
            };
            return data;
        }

        public async Task<GetCategoryDTO> GetOneAsync(int id)
        {
            var category = await categoryRepo.GetOneAsync(id);
            return mapper.Map<GetCategoryDTO>(category);
        }

        public async Task<ResultView<CategoryDTO>> UpdateAsync(CategoryDTO entity)
        {
            ResultView<CategoryDTO> result = new ResultView<CategoryDTO>();
            try
            {
                var existing = await categoryRepo.GetOneAsync(entity.Id);

                if (existing == null)
                {
                    result = new()
                    {
                        Entity = null,
                        ISSuccess = false,
                        Message = "Category Not Found or Deleted"
                    };
                    return result;
                }

                mapper.Map(entity, existing);
                await categoryRepo.UpdateAsync(existing);
                await categoryRepo.SaveChangesAsync();

                var updatedProduct = await categoryRepo.GetOneAsync(existing.Id);
                var category = mapper.Map<CategoryDTO>(updatedProduct);
                result = new()
                {
                    Entity = category,
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
