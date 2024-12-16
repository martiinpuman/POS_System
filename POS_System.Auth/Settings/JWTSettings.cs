using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_System.Auth.Settings
{
    public interface IJWTSettings
    {
        string Key { get; set; }
        string Issuer { get; set; }
        string Audience { get; set; }
        TimeSpan ExpirationTime { get; set; }
    }

    public class JWTSettings : IJWTSettings
    {
        public required string Key { get; set; }
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
        public required TimeSpan ExpirationTime { get; set; }

    }
}
