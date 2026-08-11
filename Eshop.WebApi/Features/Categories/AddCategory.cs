using Eshop.Domain;
using Eshop.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Eshop.WebApi.Features.Categories
{
    public class AddCategory
    {
        public record Command(AddCategoryRequestDto Request) : IRequest<AddCategoryResponseDto>;

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Request.Title).NotEmpty().MaximumLength(50);
                RuleFor(x => x.Request.Description).NotEmpty().MaximumLength(500);
            }
        }

        public class Handler : IRequestHandler<Command, AddCategoryResponseDto>
        {
            private readonly EshopDbContext dbContext;

            public Handler(EshopDbContext dbContext)
            {
                this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            }

            public async Task<AddCategoryResponseDto> Handle(Command command, CancellationToken cancellationToken)
            {
                var request = command.Request;
                var category = new Category(0, request.Title, request.Description);
                
                var result = await dbContext.Categories.AddAsync(category, cancellationToken);
                
                await dbContext.SaveChangesAsync(cancellationToken);
                
                return AddCategoryResponseDto.Map(result);
            }
        }
    }

    public class AddCategoryRequestDto
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
    }

    public class AddCategoryResponseDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }

        internal static AddCategoryResponseDto Map(EntityEntry<Category> result)
        {
            return new AddCategoryResponseDto
            {
                Id = result.Entity.Id,
                Title = result.Entity.Title,
                Description = result.Entity.Description
            };
        }
    }
}
