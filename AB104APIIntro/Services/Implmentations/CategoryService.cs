using AB104APIIntro.DTOs;
using AB104APIIntro.Entities;
using AB104APIIntro.Exceptions;
using AB104APIIntro.Repositories.Interfaces;
using AB104APIIntro.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AB104APIIntro.Services.Implmentations;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task CreateAsync(CategoryPostDto dto)
    {
        bool isExist = await _categoryRepository.IsExistAsync(x => x.Name == dto.Name);
        if (isExist)
            throw new CategoryAlreadyExistException();
        Category category = new() { Name = dto.Name };
        await _categoryRepository.CreateAsync(category);
        await _categoryRepository.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var category = await _categoryRepository.GetSingleAsync(x => x.Id == id);
        if (category is null)
            throw new CategoryNotFoundException();

        _categoryRepository.Delete(category);
        await _categoryRepository.SaveAsync();
    }

    public async Task<List<CategoryGetDto>> GetAllAsync()
    {
        var categories = await _categoryRepository.OrderBy((_categoryRepository.GetAllAsync()), x => x.Id).ToListAsync();
        List<CategoryGetDto> dtos = new();
        foreach (var item in categories)
        {
            CategoryGetDto dto = new() { Id = item.Id, Name = item.Name };
            dtos.Add(dto);
        }
        return dtos;
    }

    public async Task<List<CategoryGetDto>> GetAllAsync(int limit, int page)
    {
        var categories = await _categoryRepository.Paginate((_categoryRepository.GetAllAsync()), limit, page).ToListAsync();
        List<CategoryGetDto> dtos = new();
        foreach (var item in categories)
        {
            CategoryGetDto dto = new() { Id = item.Id, Name = item.Name };
            dtos.Add(dto);
        }
        return dtos;
    }

    public async Task<CategoryGetDto> GetAsync(int id)
    {
        var category = await _categoryRepository.GetSingleAsync(x => x.Id == id);
        if (category is null)
            throw new CategoryNotFoundException();
        CategoryGetDto dto = new() { Id = category.Id, Name = category.Name };

        return dto;
    }

    public async Task UpdateAsync(CategoryPutDto dto)
    {
        var existedCategory=await _categoryRepository.GetSingleAsync(x=>x.Id==dto.Id);
        if (existedCategory is null)
            throw new CategoryNotFoundException();

        bool isExist=await _categoryRepository.IsExistAsync(x=>x.Name==dto.Name && x.Id!=dto.Id);
        if (isExist)
            throw new CategoryAlreadyExistException();

        existedCategory.Name= dto.Name;
        _categoryRepository.Update(existedCategory);
        await _categoryRepository.SaveAsync();
    }

}
