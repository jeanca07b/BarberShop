using System.Text.RegularExpressions;

namespace BarberShop.Domain.ValueObjects
{
    public sealed class PhoneNumber
    {
        public string Value { get; }

        private PhoneNumber(string value)
        {
            Value = value;
        }

        public static PhoneNumber Create(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                throw new ArgumentException("Phone number is required");

            var regex = new Regex(@"^\+?[1-9]\d{7,14}$");

            if (!regex.IsMatch(phone))
                throw new ArgumentException("Invalid phone number format");

            return new PhoneNumber(phone);
        }
    }
}