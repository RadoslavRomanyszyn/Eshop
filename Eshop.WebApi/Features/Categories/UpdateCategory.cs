using Eshop.Domain;
using Eshop.Persistence;
using Eshop.WebApi.Exceptions;
using FluentValidation;
using MediatR;

namespace Eshop.WebApi.Features.Categories
{
    public class UpdateCategory
    {
        public record Command(int Id, UpdateCategoryRequestDto Request) : IRequest<UpdateCategoryResponseDto>;

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Id).GreaterThan(0);
                RuleFor(x => x.Request.Title).NotEmpty().MaximumLength(50);
                RuleFor(x => x.Request.Description).NotEmpty().MaximumLength(500);
            }
        }

        public class Handler : IRequestHandler<Command, UpdateCategoryResponseDto>
        {
            private readonly EshopDbContext dbContext;

            public Handler(EshopDbContext dbContext)
            {
                this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            }

            public async Task<UpdateCategoryResponseDto> Handle(Command command, CancellationToken cancellationToken)
            {
                var category = await dbContext.Categories.FindAsync(command.Id, cancellationToken);
                if (category is null)
                {
                    throw new NotFoundException($"Category with Id {command.Id} was not found.");
                }

                var request = command.Request;
                category.Update(request.Title, request.Description);

                await dbContext.SaveChangesAsync(cancellationToken);

                return UpdateCategoryResponseDto.Map(category);
            }
        }
    }

    public class UpdateCategoryRequestDto
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
    }

    public class UpdateCategoryResponseDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }

        internal static UpdateCategoryResponseDto Map(Category result)
        {
            return new UpdateCategoryResponseDto
            {
                Id = result.Id,
                Title = result.Title,
                Description = result.Description
            };
        }
    }
}
