using EcomGalaxy.Domain.Models.Payment;

namespace EcomGalaxy.DataAccess.Repositories.IRepository
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetAllPaymentsAsync();
        Task<Payment> GetPaymentByIdAsync(int PayId);
        Task<bool?> AddPaymentAsync(Payment Payment);
        Task<bool?> UpdatePaymentAsync(int PayId, Payment entity);
        Task<bool?> DeletePaymentAsync(int PayId);
    }
}
