using FluentValidation;
using FluentValidation.Results;

namespace Eshop.WebApi.Exceptions
{
    public class NotFoundException : ValidationException
    {
        public NotFoundException(string message, IEnumerable<ValidationFailure>? errors = null) : base(message, errors)
        {
        }
    }
}
