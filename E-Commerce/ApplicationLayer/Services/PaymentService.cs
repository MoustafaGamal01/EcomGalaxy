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

        public async Task AddPaymentAsync(Payment payment)
            => await _paymentRepository.AddPaymentAsync(payment);

        public async Task UpdatePaymentAsync(int paymentId, Payment payment)
            => await _paymentRepository.UpdatePaymentAsync(paymentId, payment);

        public async Task DeletePaymentAsync(int paymentId)
            => await _paymentRepository.DeletePaymentAsync(paymentId);

        public async Task<Payment?> GetPaymentByIdAsync(int paymentId)
            => await _paymentRepository.GetPaymentByIdAsync(paymentId);

     
        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
            => await _paymentRepository.GetAllPaymentsAsync();

        public async Task<IEnumerable<Payment>> GetPaymentsByIdsAsync(IEnumerable<int> paymentIds)
            => await _paymentRepository.GetPaymentsByIdsAsync(paymentIds);
    }
}