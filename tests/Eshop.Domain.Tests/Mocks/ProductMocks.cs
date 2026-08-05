namespace Eshop.Domain.Tests.Mocks
{
    public static class ProductMocks
    {
        public static Product GetProduct1() => new(1, "Product 1", "Description 1", 10.99m, CategoryMocks.GetCategory1());
        public static Product GetProduct2() => new(2, "Product 2", "Description 2", 20.99m, CategoryMocks.GetCategory2());
    }
}
