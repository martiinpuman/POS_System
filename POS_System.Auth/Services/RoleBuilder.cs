using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using POS_System.Auth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_System.Auth.Services
{
    public interface IRoleBuilder
    {

        Role Role { get; }

        Task<Role> Build(CancellationToken ct = default);
    }
    public class RoleBuilder : IRoleBuilder
    {
        public HashSet<Guid> PermissionIds { get; private set; } = new HashSet<Guid>();
        public Role Role { get; private set; }
        private readonly AuthDbContext _context;

        public RoleBuilder(AuthDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Asynchronously builds the Role object by adding the specified permissions and saving it to the database.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the built Role object.
        /// </returns>
        /// <exception cref="Exception">
        /// Thrown when there is an error while building the role.
        /// </exception>
        /// <remarks>
        /// This method first checks if there are any permission IDs specified. If there are, it retrieves the corresponding
        /// permissions from the database and assigns them to the Role object. Then, it adds the Role object to the Roles
        /// DbSet and saves the changes to the database.
        /// </remarks>
        public async Task<Role> Build(CancellationToken ct = default)
        {
            if (this.PermissionIds.Any())
            {
                this.Role.Permissions = await _context.Permissions.Where(p => this.PermissionIds.Contains(p.Id)).ToListAsync();

            }
            await this._context.Roles.AddAsync(this.Role);
            await this._context.SaveChangesAsync();
            return this.Role;
        }

        public IRoleBuilder AddPermission(Guid PermissionId)
        {
            this.PermissionIds.Add(PermissionId);
            return this;
        }

        public IRoleBuilder AddPermissions(IEnumerable<Guid> PermissionIds)
        {
            this.PermissionIds.UnionWith(PermissionIds);
            return this;
        }

        public IRoleBuilder SetName(string name)
        {
            this.Role.Name = name;
            return this;
        }

        public IRoleBuilder SetDescription(string description)
        {
            this.Role.Description = description;
            return this;
        }

    }
}
