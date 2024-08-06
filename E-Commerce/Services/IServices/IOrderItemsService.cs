namespace EcomGalaxy.Services.IServices
{
    public interface IOrderItemsService
    {
        Task AddOrderItemAsync(OrderItem orderItem);
        Task DeleteOrderItemAsync(int orderItemId);
        Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync();
        Task<OrderItem> GetOrderItemByIdAsync(int orderItemId);
        Task<List<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId);
        Task<IEnumerable<OrderItem>> GetOrderItemsByUserIdAsync(string sellerId);
        Task UpdateOrderItemAsync(int orderItemId, OrderItem orderItem);
    }
}
