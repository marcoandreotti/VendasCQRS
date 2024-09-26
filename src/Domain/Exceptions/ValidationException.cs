using FluentValidation.Results;

namespace Domain.Exceptions;

public class ValidationException : Exception
{
    public ValidationException() : base("Uma ou mais falhas ocorreram")
    {
        Errors = new List<string>();
    }

    public List<string> Errors { get; }

    public ValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        foreach (var failure in failures)
        {
            Errors.Add(failure.ErrorMessage);
        }
    }

}
