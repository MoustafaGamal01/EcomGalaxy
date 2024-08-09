using EcomGalaxy.ApplicationLayer.Services.IServices;
using EcomGalaxy.DataAccess.Repositories.IRepository;
using EcomGalaxy.Domain.Models.Order;
using EcomGalaxy.Domain.Models.Payment;
using EcomGalaxy.Domain.Models.ShoppingCart;
using EcomGalaxy.ViewModel.Order;

namespace EcomGalaxy.ApplicationLayer.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemsRepository _orderItemsRepository;

        public ShoppingCartService(IShoppingCartRepository cartRepository,
            IProductRepository productRepository, IPaymentRepository paymentRepository,
            IOrderRepository orderRepository, IOrderItemsRepository orderItemsRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _paymentRepository = paymentRepository;
            _orderRepository = orderRepository;
            _orderItemsRepository = orderItemsRepository;
        }

        public async Task<ShoppingCart> GetOrCreateShoppingCartAsync(string userId)
        {
            var cart = await _cartRepository.GetShoppingCartByUserIdAsync(userId);
            if (cart == null)
            {
                cart = new ShoppingCart { ApplicationUserId = userId };
                await _cartRepository.AddShoppingCartAsync(cart);
                await _cartRepository.SaveChangesAsync();
            }
            return cart;
        }

        public async Task AddToCartAsync(string userId, int productId)
        {
            var cart = await GetOrCreateShoppingCartAsync(userId);
            var cartItem = await _cartRepository.GetShoppingCartItemAsync(cart.Id, productId);

            if (cartItem == null)
            {
                cartItem = new ShoppingCartItem
                {
                    ShoppingCartId = cart.Id,
                    ProductId = productId,
                    Quantity = 1
                };
                await _cartRepository.AddShoppingCartItemAsync(cartItem);
            }
            else
            {
                cartItem.Quantity += 1;
            }

            await _cartRepository.SaveChangesAsync();
        }

        public async Task RemoveFromCartAsync(int productId, int cartId)
        {
            var cartItem = await _cartRepository.GetShoppingCartItemAsync(cartId, productId);
            if (cartItem != null)
            {
                await _cartRepository.RemoveShoppingCartItemAsync(cartItem);
                await _cartRepository.SaveChangesAsync();
            }
        }

        public async Task UpdateCartItemQuantityAsync(int producId, int cartId, int quantity)
        {
            var cartItem = await _cartRepository.GetShoppingCartItemAsync(cartId, producId);
            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                await _cartRepository.SaveChangesAsync();
            }
        }

        public async Task ClearShoppingCart(string userId)
        {
            var cart = await _cartRepository.GetShoppingCartByUserIdAsync(userId);
            if (cart != null)
            {
                await _cartRepository.ClearCartAsync(cart.Id);
                await _cartRepository.SaveChangesAsync();
            }
        }

        private Payment Pay(string userId, OrderCheckOutViewModel orderVM)
        {
            var payment = new Payment
            {
                Name = orderVM.Name,
                CardNumber = orderVM.CardNumber,
                CVV = orderVM.CVV,
                ExpiryDate = orderVM.ExpiryDate,
                CustomerId = userId,
                PaymentDate = DateTime.Now,
                Status = orderVM.PaymentStatus
            };
            return payment;
        }

        private Order MakeOrder(string userId, ShoppingCart cart)
        {
            var order = new Order
            {
                CustomerId = userId,
                OrderedDate = DateTime.Now,
                //ShippedDate = DateTime.Now,
                Status = OrderStatus.Processing,
                ShoppingCartId = cart.Id
            };
            return order;
        }

        public async Task Checkout(string userId, OrderCheckOutViewModel orderVM)
        {
            // make payment
            var payment = Pay(userId, orderVM);
            if (payment == null)
            {
                throw new Exception("Payment could not be processed.");
            }

            // find the cart
            var cart = await GetOrCreateShoppingCartAsync(userId);
            if (cart == null || !cart.ShoppingCartItems.Any())
            {
                throw new Exception("Shopping cart is empty.");
            }

            // Create order
            var order = MakeOrder(userId, cart);
            // total price of the order
            order.TotalPrice = cart.ShoppingCartItems.Sum(i => i.Product.Price * i.Quantity);

            payment.Amount = order.TotalPrice;

            await _paymentRepository.AddPaymentAsync(payment);

            order.PaymentId = payment.Id;

            order.Payment = payment;

            await _orderRepository.AddOrderAsync(order);

            await _cartRepository.SaveChangesAsync();

            // add orderItems & update Qtty of prds
            foreach (var cartItem in cart.ShoppingCartItems)
            {
                var product = await _productRepository.GetProductByIdAsync(cartItem.ProductId);
                if (product == null)
                {
                    throw new Exception("Product not found.");
                }

                // Update product stock
                if (product.StockQuantity < cartItem.Quantity)
                {
                    throw new Exception($"Not enough stock for product {product.Name}.");
                }
                product.StockQuantity -= cartItem.Quantity;
                await _productRepository.UpdateProductAsync(product.Id, product);

                // Create order item
                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    Price = product.Price * cartItem.Quantity,
                    SellerId = product.ApplicationUserId
                };

                await _orderItemsRepository.AddOrderItemAsync(orderItem);
                order.OrderItems.Add(orderItem);
            }

            await _orderRepository.UpdateOrderAsync(order.Id, order);

            await _cartRepository.SaveChangesAsync();

            await _cartRepository.ClearCartAsync(cart.Id);
        }

    }
}
