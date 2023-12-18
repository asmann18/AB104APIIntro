namespace AB104APIIntro.Exceptions;

public class CategoryAlreadyExistException:Exception
{
    public CategoryAlreadyExistException(string message="Category is already exist"):base(message)
    {
        
    }
}
