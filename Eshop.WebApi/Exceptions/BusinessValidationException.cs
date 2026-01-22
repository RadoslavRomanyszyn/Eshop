using FluentValidation;
using FluentValidation.Results;

namespace Eshop.WebApi.Exceptions
{
    public class BusinessValidationException : ValidationException
    {
        public BusinessValidationException(string message, IEnumerable<ValidationFailure>? errors = null) : base(message, errors)
        {
        }
    }
}
