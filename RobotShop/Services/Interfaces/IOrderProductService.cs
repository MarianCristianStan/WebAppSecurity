using RobotShop.Models;

namespace RobotShop.Services.Interfaces
{
    public interface IOrderProductService : IGenericServiceRepo<OrderProduct>
    {
        List<OrderProduct> GetOrderProductsByOrderId(string orderId);
        void DeleteCompositeKey(string orderId, string productId);
    }
}
