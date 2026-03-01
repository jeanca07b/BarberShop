using BarberShop.Domain.Common;
using BarberShop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; private set; } = null!;
        public string PasswordHash { get; private set; } = null!;
        public UserRole Role { get; private set; }

        public bool IsActive { get; private set; }

        private User() { }

        public User(string email, string passwordHash, UserRole role)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required");

            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentException("Password hash is required");

            Email = email;
            PasswordHash = passwordHash;
            Role = role;
            IsActive = true;
        }

        public void ChangeRole(UserRole role)
        {
            Role = role;
        }

        public void Deactivate() => IsActive = false;
        public void Activate() => IsActive = true;
    }
}
