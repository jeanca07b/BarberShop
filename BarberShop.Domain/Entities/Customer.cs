using BarberShop.Domain.Common;
using BarberShop.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string FirstName { get; private set; } = null!;
        public string LastName { get; private set; } = null!;
        public Email Email { get; private set; } = null!;
        public PhoneNumber PhoneNumber { get; private set; } = null!;

        public Guid UserId { get; private set; }

        private Customer() { }

        private Customer(string firstName, string lastName, Email email, PhoneNumber phoneNumber, Guid userId)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            UserId = userId;
        }

        public static Customer Create(string firstName, string lastName, Email email, PhoneNumber phoneNumber, Guid userId)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name is required");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name is required");

            return new Customer(firstName, lastName, email, phoneNumber, userId);
        }

        public void UpdateName(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name is required");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name is required");

            FirstName = firstName;
            LastName = lastName;
        }

        public void UpdateEmail(Email email)
        {
            Email = email;
        }

        public void UpdatePhone(PhoneNumber phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }
    }
}
