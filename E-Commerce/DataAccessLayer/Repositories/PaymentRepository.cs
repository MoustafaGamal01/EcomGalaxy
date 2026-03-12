using EcomGalaxy.DataAccess.Repositories.IRepository;
using EcomGalaxy.Domain.Models.Context;
using EcomGalaxy.Domain.Models.Payment;
using Microsoft.EntityFrameworkCore;

namespace EcomGalaxy.DataAccess.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly MyContext _context;

        public PaymentRepository(MyContext context)
        {
            _context = context;
        }


        public async Task AddPaymentAsync(Payment payment)
        {
            if (payment == null) throw new ArgumentNullException(nameof(payment));

            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePaymentAsync(int paymentId, Payment payment)
        {
            if (payment == null) throw new ArgumentNullException(nameof(payment));

            var existing = await _context.Payments.FindAsync(paymentId)
                ?? throw new KeyNotFoundException($"Payment {paymentId} not found.");

            _context.Entry(existing).CurrentValues.SetValues(payment);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePaymentAsync(int paymentId)
        {
            var existing = await _context.Payments.FindAsync(paymentId)
                ?? throw new KeyNotFoundException($"Payment {paymentId} not found.");

            _context.Payments.Remove(existing);
            await _context.SaveChangesAsync();
        }


        public async Task<Payment?> GetPaymentByIdAsync(int paymentId)
        {
            return await _context.Payments
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == paymentId);
        }



        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
        {
            return await _context.Payments
                .AsNoTracking()
                .ToListAsync();
        }

    
        public async Task<IEnumerable<Payment>> GetPaymentsByIdsAsync(IEnumerable<int> paymentIds)
        {
            if (paymentIds == null || !paymentIds.Any())
                return Enumerable.Empty<Payment>();

            return await _context.Payments
                .AsNoTracking()
                .Where(p => paymentIds.Contains(p.Id))
                .ToListAsync();
        }
    }
}