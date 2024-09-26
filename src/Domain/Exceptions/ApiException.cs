namespace Domain.Exceptions;

public class ApiException : Exception
{
    public ApiException() : base() { }

    public bool SendLogger { get; set; }

    public string CodeApiError { get; set; }

    public List<string> ConcatenatedString { get; set; }

    public ApiException(string message, bool setLogger = false) : base(message)
    {
        if (setLogger)
        {
            SendLogger = true;
        }
    }

    public ApiException(string codeApiError, bool setLogger, string[] concatenatedString) : base("")
    {
        if (setLogger)
        {
            SendLogger = true;
        }

        CodeApiError = codeApiError;
        ConcatenatedString = concatenatedString.ToList();
    }

    public ApiException(string codeApiError, string concatenatedString, bool setLogger = false) : base("")
    {
        if (setLogger)
        {
            SendLogger = true;
        }

        CodeApiError = codeApiError;
        ConcatenatedString = new List<string>
            {
                concatenatedString
            };
    }

    public List<string> Errors { get; }

    public ApiException(List<string> erros) : base("Uma ou mais falhas ocorreram")
    {
        Errors = erros;
    }

}
