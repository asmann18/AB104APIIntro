using AB104APIIntro.DTOs;
using AB104APIIntro.Entities;
using AB104APIIntro.Exceptions;
using AB104APIIntro.Repositories.Interfaces;
using AB104APIIntro.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AB104APIIntro.Services.Implmentations;

public class TagService : ITagService
{
    private readonly ITagRepository _tagRepository;

    public TagService(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public async Task CreateAsync(TagPostDto dto)
    {
        bool isExist = await _tagRepository.IsExistAsync(x => x.Name == dto.Name);
        if (isExist)
            throw new TagAlreadyExistException();

        Tag tag = new() { Name = dto.Name };
        await _tagRepository.CreateAsync(tag);
        await _tagRepository.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var tag = await _tagRepository.GetSingleAsync(x => x.Id == id);
        if (tag is null)
            throw new TagNotFoundException();
        _tagRepository.Delete(tag);
        await _tagRepository.SaveAsync();
    }

    public async Task<List<TagGetDto>> GetAllAsync()
    {
        var tags = await _tagRepository.OrderBy(_tagRepository.GetAllAsync(), x => x.Id).ToListAsync();
        List<TagGetDto> dtos = new();
        foreach (var tag in tags)
        {
            TagGetDto dto = new() { Id = tag.Id, Name = tag.Name };
            dtos.Add(dto);
        }
        return dtos;
    }

    public async Task<List<TagGetDto>> GetAllAsync(int limit, int page)
    {
        var tags = await _tagRepository.Paginate(_tagRepository.GetAllAsync(), limit, page).ToListAsync();
        List<TagGetDto> dtos = new();
        foreach (var tag in tags)
        {
            TagGetDto dto = new() { Id = tag.Id, Name = tag.Name };
            dtos.Add(dto);
        }
        return dtos;
    }

    public async Task<TagGetDto> GetAsync(int id)
    {
        var tag = await _tagRepository.GetSingleAsync(x => x.Id == id);
        if (tag is null)
            throw new TagNotFoundException();
        TagGetDto dto = new() { Id = tag.Id, Name = tag.Name };
        return dto;
    }

    public async Task UpdateAsync(TagPutDto dto)
    {
        var existedTag = await _tagRepository.GetSingleAsync(x => x.Id == dto.Id);
        if (existedTag is null)
            throw new TagNotFoundException();

        bool isExist = await _tagRepository.IsExistAsync(x => x.Name == dto.Name && x.Id != dto.Id);
        if (isExist)
            throw new TagAlreadyExistException();

        existedTag.Name = dto.Name;
        _tagRepository.Update(existedTag);
        await _tagRepository.SaveAsync();
    }
}
