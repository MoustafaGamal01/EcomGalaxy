using EcomGalaxy.Domain.Models.Order;

namespace EcomGalaxy.DataAccess.Repositories.IRepository
{
    public interface IOrderItemsRepository
    {
        Task<bool?> AddOrderItemAsync(OrderItem orderItem);
        Task<bool?> DeleteOrderItemAsync(int orderItemId);
        Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync();
        Task<OrderItem> GetOrderItemByIdAsync(int orderItemId);
        Task<List<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId);
        Task<IEnumerable<OrderItem>> GetOrderItemsByUserIdAsync(string sellerId);
        Task<bool?> UpdateOrderItemAsync(int orderItemId, OrderItem orderItem);
    }
}
