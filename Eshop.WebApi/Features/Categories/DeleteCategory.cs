using Eshop.Persistence;
using Eshop.WebApi.Exceptions;
using FluentValidation;
using MediatR;

namespace Eshop.WebApi.Features.Categories
{
    public class DeleteCategory
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
                var category = await dbContext.Categories.FindAsync(command.Id, cancellationToken);
                if (category is null)
                {
                    throw new NotFoundException($"Category with Id {command.Id} was not found.");
                }

                // NOTE: there is no async version of Remove
                dbContext.Categories.Remove(category);

                await dbContext.SaveChangesAsync(cancellationToken);
                
                return Unit.Value;
            }
        }
    }
}
