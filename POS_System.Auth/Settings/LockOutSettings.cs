using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_System.Auth.Settings
{
    public interface ILockOutSettings
    {
        int MaxFailedAccessAttempts { get; set; }
        TimeSpan DefaultLockoutTimeSpan { get; set; }
    }

    public class LockOutSettings : ILockOutSettings
    {
        public int MaxFailedAccessAttempts { get; set; }
        public TimeSpan DefaultLockoutTimeSpan { get; set; }
    }
}
