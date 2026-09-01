using Eshop.Domain;

namespace Eshop.Tests.Common.Mocks
{
    public static class ProductMocks
    {
        public static Product GetProduct1() => new(1, "Product 1", "Product Description 1", 10.99m, CategoryMocks.GetCategory1());
        public static Product GetProduct2() => new(2, "Product 2", "Product Description 2", 20.99m, CategoryMocks.GetCategory2());
        public static Product GetProduct3() => new(3, "Product 3", "Product Description 3", 30.99m, CategoryMocks.GetCategory3());
    }
}
