namespace MyRecipeBook.Exceptions.ExceptionsBase;

public class InvalidLoginException : MyRecipeBookException
{
    public InvalidLoginException() : base(Messages.email_or_password_invalid)
    {
    }

}