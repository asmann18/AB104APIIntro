﻿namespace AB104APIIntro.Exceptions;

public class TagNotFoundException:Exception
{
    public TagNotFoundException(string message="Tag is not found"):base(message)
    {
        
    }
}
