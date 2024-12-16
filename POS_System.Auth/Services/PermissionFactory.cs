using POS_System.Auth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_System.Auth.Services
{
    public interface IPermissionFactory
    {
        Permission Create();
    }
    public class PermissionFactory : IPermissionFactory
    {
        public Permission Create()
        {
            throw new NotImplementedException();
        }
    }
}
