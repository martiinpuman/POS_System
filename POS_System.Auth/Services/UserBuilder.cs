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
        Task<ApplicationUser> Build(CancellationToken ct = default);
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
        public ApplicationUser User { get; private set; }

        private readonly AuthDbContext _context;

        public UserBuilder(AuthDbContext context)
        {
            _context = context;
        }



        /// <summary>
        /// Asynchronously builds the ApplicationUser object by adding it to the database and saving the changes.
        /// </summary>
        /// <param name="ct">A CancellationToken to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the built ApplicationUser object.
        /// If the operation is canceled, the task result is null.
        /// </returns>
        /// <exception cref="Exception">
        /// Thrown when there is an error while building the user.
        /// </exception>
        /// <remarks>
        /// This method first checks if the cancellation token has requested cancellation. If it has, the method returns null.
        /// Otherwise, it adds the ApplicationUser object to the Users DbSet and saves the changes to the database.
        /// </remarks>
        public async Task<ApplicationUser> Build(CancellationToken ct = default)
        {
            if (ct.IsCancellationRequested)
            {
                return null;
            }
            await this._context.Users.AddAsync(this.User, ct);
            await this._context.SaveChangesAsync(ct);
            return this.User;
        }

        public IUserBuilder SetEmail(string email)
        {
            this.User.Email = email;
            return this;
        }

        public IUserBuilder SetUsername(string username)
        {
            this.User.UserName = username;
            return this;
        }

        public IUserBuilder SetPassword(string password)
        {
            this.User.PasswordHash = password;
            return this;
        }

        public IUserBuilder AddRole(Role role)
        {
            this.User.Role.ToList().Add(role);

            return this;
        }

        public IUserBuilder AddRoles(IEnumerable<Role> roles)
        {
            this.User.Role.ToList().AddRange(roles);
            return this;
        }

        public IUserBuilder AddPermissions(IEnumerable<Permission> permissions)
        {
            this.User.Permissions = permissions;
            return this;
        }

        public IUserBuilder AddPermission(Permission permission)
        {
            this.User.Permissions = this.User.Permissions.Append(permission);
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
