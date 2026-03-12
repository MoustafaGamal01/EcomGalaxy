using EcomGalaxy.Domain.Models.Payment;

namespace EcomGalaxy.ApplicationLayer.Services.IServices
{
    public interface IPaymentService
    {
        // Write
        Task AddPaymentAsync(Payment payment);
        Task UpdatePaymentAsync(int paymentId, Payment payment);
        Task DeletePaymentAsync(int paymentId);

        // Read — single
        Task<Payment?> GetPaymentByIdAsync(int paymentId);
        // Read — collections
        Task<IEnumerable<Payment>> GetAllPaymentsAsync();

        // Batch — used by OrderService.CustomerOrders
        Task<IEnumerable<Payment>> GetPaymentsByIdsAsync(IEnumerable<int> paymentIds);
    }
}