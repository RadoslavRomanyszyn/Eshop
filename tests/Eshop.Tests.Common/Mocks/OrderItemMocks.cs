using Eshop.Domain;

namespace Eshop.Tests.Common.Mocks
{
    public static class OrderItemMocks
    {
        public static OrderItem GetOrderItem1() => new(1, 1.23m, ProductMocks.GetProduct1());
        public static OrderItem GetOrderItem2() => new(2, 4.56m, ProductMocks.GetProduct2());
        public static OrderItem GetOrderItem3() => new(3, 7.89m, ProductMocks.GetProduct3());
    }
}
