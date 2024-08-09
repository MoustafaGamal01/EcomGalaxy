using EcomGalaxy.Domain.Models.Order;

namespace EcomGalaxy.DataAccess.Repositories.IRepository
{
    public interface IOrderRepository
    {
        Task<bool?> AddOrderAsync(Order order);

        Task<bool?> UpdateOrderAsync(int orderId, Order order);

        Task<bool?> DeleteOrderAsync(int orderId);

        Task<IEnumerable<Order>> GetAllOrdersAsync();

        Task<Order> GetOrderByIdAsync(int orderId);

        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId);
    }
}
