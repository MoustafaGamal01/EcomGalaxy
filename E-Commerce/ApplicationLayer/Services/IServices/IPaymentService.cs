using EcomGalaxy.Domain.Models.Payment;

namespace EcomGalaxy.ApplicationLayer.Services.IServices
{
    public interface IPaymentService
    {
        Task<IEnumerable<Payment>> GetAllPaymentsAsync();
        Task<Payment> GetPaymentByIdAsync(int id);
        Task<bool?> AddPaymentAsync(Payment Payment);
        Task<bool?> UpdatePaymentAsync(int PayId, Payment entity);
        Task<bool?> DeletePaymentAsync(int PayId);
    }
}
