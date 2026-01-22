using Eshop.Domain;
using Eshop.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Eshop.WebApi.Features.Products
{
    public class AddProduct
    {
        public record Command(AddProductRequestDto Request) : IRequest<AddProductResponseDto>;

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Request.Title).NotEmpty().MaximumLength(50);
                RuleFor(x => x.Request.Description).NotEmpty().MaximumLength(500);
                RuleFor(x => x.Request.Price).GreaterThanOrEqualTo(0);
                // NOTE: CategoryId is optional so no validation rule for it
            }
        }
        
        public class Handler : IRequestHandler<Command, AddProductResponseDto>
        {
            private readonly EshopDbContext dbContext;

            public Handler(EshopDbContext dbContext)
            {
                this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            }

            public async Task<AddProductResponseDto> Handle(Command command, CancellationToken cancellationToken)
            {
                var request = command.Request;
                var category = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == request.CategoryId, cancellationToken);
                var product = new Product(0, request.Title, request.Description, request.Price, category);

                var result = await dbContext.Products.AddAsync(product, cancellationToken);

                await dbContext.SaveChangesAsync(cancellationToken);

                return AddProductResponseDto.Map(result);
            }
        }
    }

    public class AddProductRequestDto
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public int? CategoryId { get; set; }
    }

    public class AddProductResponseDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public AddProductCategoryDto? Category { get; set; }

        public class AddProductCategoryDto
        {
            public int Id { get; set; }
            public required string Title { get; set; }
            public required string Description { get; set; }
        }

        internal static AddProductResponseDto Map(EntityEntry<Product> result)
        {
            return new AddProductResponseDto
            {
                Id = result.Entity.Id,
                Title = result.Entity.Title,
                Description = result.Entity.Description,
                Price = result.Entity.Price,
                Category = result.Entity.Category != null ? new AddProductCategoryDto
                {
                    Id = result.Entity.Category.Id,
                    Title = result.Entity.Category.Title,
                    Description = result.Entity.Category.Description
                } : null
            };
        }
    }
}
