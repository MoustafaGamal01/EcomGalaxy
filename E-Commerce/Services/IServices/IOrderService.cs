using EcomGalaxy.ViewModel.Order;

namespace EcomGalaxy.Services.IServices
{
    public interface IOrderService
    {
        Task<bool?> AddOrderAsync(Order order);

        Task<bool?> UpdateOrderAsync(int orderId, Order order);

        Task<bool?> DeleteOrderAsync(int orderId);

        Task<IEnumerable<Order>> GetAllOrdersAsync();

        Task<Order> GetOrderByIdAsync(int orderId);

        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId);

        Task<IEnumerable<CustomerOrderItemViewModel>> OrderDetails(int orderId);

        Task<IEnumerable<OrdersMainViewModel>> CustomerOrders(string userId);

        Task<IEnumerable<SellerOrderItemViewModel>> OrderSellerDetails(string userId);

        Task<IEnumerable<AdminOrderItemViewModel>> OrderAdminDetails();
    }
}
