namespace BarberShop.Application.DTOs.Customer
{
    public class CustomerResponse
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;
    }
}
