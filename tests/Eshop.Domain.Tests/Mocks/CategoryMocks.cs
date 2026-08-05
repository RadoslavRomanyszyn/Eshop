namespace Eshop.Domain.Tests.Mocks
{
    public static class CategoryMocks
    {
        public static Category GetCategory1() => new(1, "Category 1", "Description 1");
        public static Category GetCategory2() => new(2, "Category 2", "Description 2");
    }
}
