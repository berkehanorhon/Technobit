using MediatR;
using ProductManagement.DTOs.Create;
using ProductManagement.DTOs.Read;
using ProductManagement.DTOs.Update;
using ProductManagement.Interfaces;
using ProductManagement.Models;

namespace ProductManagement.Services;


public class CategoryService : ICategoryService
{
    private readonly IRepository<Category> _categoryRepository;

    public CategoryService(IRepository<Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        return categories.Select(p => new CategoryDTO
        {
            Id = p.Id,
            Name = p.Name,
            Subcategoryid = p.Subcategoryid,
            Description = p.Description
        });
    }

    public async Task<CategoryDTO?> GetByIdAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null) return null;

        return new CategoryDTO
        {
            Id = category.Id,
            Name = category.Name,
            Subcategoryid = category.Subcategoryid,
            Description = category.Description
        };
    }

    public async Task<int> CreateAsync(CreateCategoryDTO createCategoryDto)
    {
        var category = new Category
        {
            Name = createCategoryDto.Name,
            Subcategoryid = createCategoryDto.Subcategoryid,
            Description = createCategoryDto.Description
        };

        await _categoryRepository.AddAsync(category);

        return category.Id;
    }

    public async Task<UpdateCategoryDTO> UpdateAsync(UpdateCategoryDTO categoryDto)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryDto.Id);
        if (category == null) return null;

        category.Name = categoryDto.Name;
        category.Subcategoryid = categoryDto.Subcategoryid;
        category.Description = categoryDto.Description;

        await _categoryRepository.UpdateAsync(category);

        return categoryDto;
    }

    public async Task<Unit> DeleteAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category != null)
        {
            await _categoryRepository.DeleteAsync(category);
        }
        return Unit.Value;
    }
}