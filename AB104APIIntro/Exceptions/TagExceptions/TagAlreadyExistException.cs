namespace AB104APIIntro.Exceptions;

public class TagAlreadyExistException:Exception
{
    public TagAlreadyExistException(string message="Tag is already exist"):base(message)
    {
        
    }
}
