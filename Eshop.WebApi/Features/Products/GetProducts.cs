using Eshop.Domain;
using Eshop.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Eshop.WebApi.Features.Products
{
    public class GetProducts
    {
        // NOTE: later CategoryId as mandatory parameter
        public record Query() : IRequest<IEnumerable<GetProductsResponseDto>>;

        public class Handler : IRequestHandler<Query, IEnumerable<GetProductsResponseDto>>
        {
            private readonly EshopDbContext dbContext;
            
            public Handler(EshopDbContext dbContext)
            {
                this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            }

            // NOTE: using IEnumerable to have a collection of multiple products=ResponseDto
            public async Task<IEnumerable<GetProductsResponseDto>> Handle(Query command, CancellationToken cancellationToken)
            {
                var products = await dbContext.ProductsViews
                    .Include(x => x.Category)
                    .ToListAsync(cancellationToken);
                
                return products.Select(GetProductsResponseDto.Map).ToList();
            }
        }
    }

    public class GetProductsResponseDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public GetProductsCategoryDto? Category { get; set; }

        public class GetProductsCategoryDto
        {
            public int Id { get; set; }
            public required string Title { get; set; }
            public required string Description { get; set; }
        }

        internal static GetProductsResponseDto Map(Product result)
        {
            return new GetProductsResponseDto
            {
                Id = result.Id,
                Title = result.Title,
                Description = result.Description,
                Price = result.Price,
                Category = result.Category != null ? new GetProductsCategoryDto
                {
                    Id = result.Category.Id,
                    Title = result.Category.Title,
                    Description = result.Category.Description
                } : null
            };
        }
    }
}
