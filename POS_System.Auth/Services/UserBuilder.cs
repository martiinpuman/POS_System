using Microsoft.EntityFrameworkCore;
using POS_System.Auth.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_System.Auth.Services
{
    public interface IUserBuilder
    {
        ApplicationUser User { get; }
        ApplicationUser Build();
        IUserBuilder SetEmail(string email);
        IUserBuilder SetUsername(string username);
        IUserBuilder SetPassword(string password);
        IUserBuilder AddRole(Role role);
        IUserBuilder AddRoles(IEnumerable<Role> roles);
        IUserBuilder AddPermissions(IEnumerable<Permission> permissions);
        IUserBuilder AddPermission(Permission permission);
        IUserBuilder SetPhoneNumber(string phonenumber);
        IUserBuilder SetFirstName(string firstname);
        IUserBuilder SetLastName(string lastname);
    }
    public class UserBuilder : IUserBuilder
    {
        public ApplicationUser User { get; private set; } = new ApplicationUser();

        private readonly AuthDbContext _context;

        public UserBuilder(AuthDbContext context)
        {
            _context = context;
        }




        /// <summary>
        /// Builds the ApplicationUser object.
        /// </summary>
        /// <returns>
        /// The built ApplicationUser object.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when  FirstNameor LastName is not set.
        /// </exception>
        public ApplicationUser Build()
        {
            // Validate required properties
            if (string.IsNullOrEmpty(User.FirstName) || string.IsNullOrEmpty(User.LastName))
            {
                throw new InvalidOperationException("FirstName and LastName are required.");
            }

            return this.User;
        }

        public IUserBuilder SetEmail(string email)
        {
            this.User.Email = email;
            return this;
        }

        public IUserBuilder SetUsername(string username)
        {
            this.User.UserName = new string(username);
            return this;
        }

        public IUserBuilder SetPassword(string password)
        {
            this.User.PasswordHash = password;
            return this;
        }

        public IUserBuilder AddRole(Role role)
        {
            if(this.User.Role == null)
            {
                this.User.Role = new List<Role>();
            }

            this.User.Role.Add(role);

            return this;
        }

        public IUserBuilder AddRoles(IEnumerable<Role> roles)
        {
            if(this.User.Role == null)
            {
                this.User.Role = new List<Role>();
            }

            foreach (Role role in roles)
            {
                this.User.Role.Add(role);
            }
            return this;
        }

        public IUserBuilder AddPermissions(IEnumerable<Permission> permissions)
        {
            if (this.User.Permissions == null)
            {
                this.User.Permissions = new List<Permission>();
            }

            foreach (var permission in permissions)
            {
                this.User.Permissions.Add(permission);
            }

            return this;
        }

        public IUserBuilder AddPermission(Permission permission)
        {
            if (this.User.Permissions == null)
            {
                this.User.Permissions = new List<Permission>();
            }

            this.User.Permissions.Add(permission);
            return this;
        }


        public IUserBuilder SetPhoneNumber(string phonenumber)
        {
            this.User.PhoneNumber = phonenumber;
            return this;
        }

        public IUserBuilder SetFirstName(string firstname)
        {
            this.User.FirstName = firstname;
            return this;
        }

        public IUserBuilder SetLastName(string lastname)
        {
            this.User.LastName = lastname;
            return this;
        }

    }
}
