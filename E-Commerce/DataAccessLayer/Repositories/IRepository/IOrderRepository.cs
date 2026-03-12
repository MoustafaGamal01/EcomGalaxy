using EcomGalaxy.Domain.Models.Order;

namespace EcomGalaxy.DataAccess.Repositories.IRepository
{
    public interface IOrderRepository
    {
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(int orderId, Order order);
        Task DeleteOrderAsync(int orderId);

        Task<Order?> GetOrderByIdAsync(int orderId);

        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId);

        Task<IEnumerable<Order>> GetOrdersByIdsAsync(IEnumerable<int> orderIds);
    }
}