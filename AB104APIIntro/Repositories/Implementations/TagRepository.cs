using AB104APIIntro.DAL;
using AB104APIIntro.Entities;
using AB104APIIntro.Repositories.Interfaces;

namespace AB104APIIntro.Repositories.Implementations;

public class TagRepository:Repository<Tag>,ITagRepository
{
    public TagRepository(AppDbContext context):base(context)
    {
        
    }
}
