using BarberShop.Domain.Common;
using BarberShop.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Domain.Entities
{
    public class Barber : BaseEntity
    {
        public string FirstName { get; private set; } = null!;
        public string LastName { get; private set; } = null!;
        public Email Email { get; private set; } = null!;
        public bool IsActive { get; private set; }


        public Barber(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = Email.Create(email);
            IsActive = true;

            Validate();
        }
        private Barber() { }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(FirstName))
                throw new ArgumentException("First name is required.");
            if (string.IsNullOrWhiteSpace(LastName))
                throw new ArgumentException("Last name is required.");
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public void Activate()
        {
            IsActive = true;
        }


    }
}
