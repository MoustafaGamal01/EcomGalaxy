using EcomGalaxy.ApplicationLayer.Services.IServices;
using EcomGalaxy.DataAccess.Repositories.IRepository;
using EcomGalaxy.Domain.Models.Payment;

namespace EcomGalaxy.ApplicationLayer.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }
        public async Task<bool?> AddPaymentAsync(Payment Payment)
        {
            return await _paymentRepository.AddPaymentAsync(Payment);
        }

        public async Task<bool?> DeletePaymentAsync(int PayId)
        {
            return await _paymentRepository.DeletePaymentAsync(PayId);
        }

        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
        {
            return await _paymentRepository.GetAllPaymentsAsync();
        }

        public async Task<Payment> GetPaymentByIdAsync(int PayId)
        {
            return await _paymentRepository.GetPaymentByIdAsync(PayId);
        }

        public async Task<bool?> UpdatePaymentAsync(int PayId, Payment entity)
        {
            return await _paymentRepository.UpdatePaymentAsync(PayId, entity);
        }
    }
}
