using Eshop.Persistence;
using Eshop.WebApi.Exceptions;
using FluentValidation;
using MediatR;

namespace Eshop.WebApi.Features.Products
{
    public class DeleteProduct
    {
        public record Command(int Id) : IRequest<Unit>;

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Id).GreaterThan(0);
            }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly EshopDbContext dbContext;

            public Handler(EshopDbContext dbContext)
            {
                this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            }

            public async Task<Unit> Handle(Command command, CancellationToken cancellationToken)
            {
                var product = await dbContext.Products.FindAsync(command.Id, cancellationToken);
                if (product is null)
                {
                    throw new NotFoundException($"Product with Id {command.Id} was not found.");
                }

                // NOTE: there is no async version of Remove
                dbContext.Products.Remove(product);

                await dbContext.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
