using AB104APIIntro.DAL;
using AB104APIIntro.Entities;
using AB104APIIntro.Repositories.Interfaces;

namespace AB104APIIntro.Repositories.Implementations
{
    public class CategoryRepository:Repository<Category>,ICategoryRepository
    {
        public CategoryRepository(AppDbContext context):base(context)
        {
            
        }
    }
}
