using EcomGalaxy.Domain.Models.Order;
using EcomGalaxy.ViewModel.Order;

namespace EcomGalaxy.ApplicationLayer.Services.IServices
{
    public interface IOrderService
    {
        // Write
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(int orderId, Order order);
        Task DeleteOrderAsync(int orderId);

        // Read
        Task<Order?> GetOrderByIdAsync(int orderId);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId);

        // Projections
        Task<IEnumerable<OrdersMainViewModel>> CustomerOrders(string userId);
        Task<IEnumerable<CustomerOrderItemViewModel>> OrderDetails(int orderId);
        Task<IEnumerable<SellerOrderItemViewModel>> OrderSellerDetails(string userId);
        Task<IEnumerable<AdminOrderItemViewModel>> OrderAdminDetails();

        // Business operations — keep logic out of the controller
        Task CancelOrderAsync(int orderId);
        Task ShipOrderAsync(int orderId);
        Task ReceivedOrderAsync(int orderId);
    }
}