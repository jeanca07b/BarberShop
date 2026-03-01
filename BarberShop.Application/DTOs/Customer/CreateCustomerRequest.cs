namespace BarberShop.Application.DTOs.Customer
{
    public class CreateCustomerRequest
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;
    }
}
