namespace AB104APIIntro.Exceptions;

public class CategoryNotFoundException:Exception
{
    public CategoryNotFoundException(string message="Category not found!!"):base(message)
    {
        
    }
}
