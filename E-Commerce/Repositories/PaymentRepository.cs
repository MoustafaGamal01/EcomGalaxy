
namespace EcomGalaxy.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly MyContext _context;

        public PaymentRepository(MyContext context)
        {
            this._context = context;
        }

        public async Task<bool?> AddPaymentAsync(Payment Payment)
        {
            _context.payments.Add(Payment);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool?> DeletePaymentAsync(int PayId)
        {
            var existPayment = await GetPaymentByIdAsync(PayId);
            if (existPayment == null)
            {
                throw new InvalidOperationException($"Payment with ID {PayId} not found.");
            }
            _context.payments.Remove(existPayment);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
        {
            return await _context.payments.ToListAsync();
        }

        public Task<Payment> GetPaymentByIdAsync(int PayId)
        {
            return _context.payments.FirstOrDefaultAsync(p => p.Id == PayId);
        }

        public async Task<bool?> UpdatePaymentAsync(int PayId, Payment Payment)
        {
            var existPayment = await GetPaymentByIdAsync(PayId);
            if (existPayment == null)
            {
                throw new InvalidOperationException($"Payment with ID {PayId} not found.");
            }
            _context.Entry(existPayment).CurrentValues.SetValues(Payment);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
