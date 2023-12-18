using AB104APIIntro.DTOs;

namespace AB104APIIntro.Services.Interfaces;

public interface ITagService
{
    Task<List<TagGetDto>> GetAllAsync();
    Task<List<TagGetDto>> GetAllAsync(int limit, int page);
    Task<TagGetDto> GetAsync(int id);

    Task CreateAsync(TagPostDto dto);
    Task UpdateAsync(TagPutDto dto);
    Task DeleteAsync(int id);
}
