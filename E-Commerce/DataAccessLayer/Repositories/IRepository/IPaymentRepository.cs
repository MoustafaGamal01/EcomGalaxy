using EcomGalaxy.Domain.Models.Payment;

namespace EcomGalaxy.DataAccess.Repositories.IRepository
{
    public interface IPaymentRepository
    {
        // Write
        Task AddPaymentAsync(Payment payment);
        Task UpdatePaymentAsync(int paymentId, Payment payment);
        Task DeletePaymentAsync(int paymentId);

        // Read — single
        Task<Payment?> GetPaymentByIdAsync(int paymentId);

        // Read — collections
        Task<IEnumerable<Payment>> GetAllPaymentsAsync();

        // Batch — avoids N+1 in OrderService.CustomerOrders
        Task<IEnumerable<Payment>> GetPaymentsByIdsAsync(IEnumerable<int> paymentIds);
    }
}