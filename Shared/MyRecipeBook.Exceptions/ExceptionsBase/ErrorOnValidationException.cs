namespace MyRecipeBook.Exceptions.ExceptionsBase;

public class ErrorOnValidationException : MyRecipeBookException
{
    public ErrorOnValidationException(IList<string> errorMessages): base(string.Empty)
    {
        ErrorMessages = errorMessages;
    }

    public IList<string> ErrorMessages { get; set; }
}